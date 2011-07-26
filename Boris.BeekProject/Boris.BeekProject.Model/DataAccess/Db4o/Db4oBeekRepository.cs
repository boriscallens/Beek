using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using Boris.BeekProject.Model.Beek;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.Linq;

namespace Boris.BeekProject.Model.DataAccess.Db4o
{
    public class Db4oBeekRepository: IBeekRepository
    {
        private readonly IObjectServer server;
        private readonly IObjectContainer client;
        private readonly object genreLock;
        private readonly object beekLock;
        private static readonly IDictionary<string, IObjectServer> servers = new Dictionary<string, IObjectServer>();

        public Db4oBeekRepository (): this(ConfigurationManager.AppSettings["beekRepository.path.db4o"]){}
        private Db4oBeekRepository (string db4oFilePath) 
        {
            db4oFilePath = EnsureDb4OFilePath(db4oFilePath);
            lock (servers)
            {
                if (!servers.ContainsKey(db4oFilePath))
                {
                    server = Db4oClientServer.OpenServer(db4oFilePath, 0);
                    servers.Add(db4oFilePath, server);
                }
            }

            server = servers[db4oFilePath];
            client = server.OpenClient(); 
            genreLock = new object();
            beekLock = new object();
        }

        public Db4oBeekRepository (IObjectServer beekServer)
        {
            server = beekServer;
            client = server.OpenClient();
            genreLock = new object();
            beekLock = new object();
        }

        public IQueryable<BaseGenre> GetGenres()
        {
            return client.AsQueryable<BaseGenre>();
        }
        public int AddGenre (BaseGenre genre)
        {
            lock (genreLock)
            {
                if (!GetGenres().Any(g => g.Equals(genre)))
                {
                    genre.Id = client.Cast<BaseGenre>().Select(g => g.Id).DefaultIfEmpty(-1).Max() + 1;
                    client.Store(genre);
                    client.Commit();
                }
            }
            return genre.Id;
        }
        public void RemoveGenre (BaseGenre genre)
        {
            lock (genreLock)
            {
                client.Delete(genre);    
                client.Commit();
            }
        }
        public void UpdateGenre (BaseGenre g)
        {
            // ToDo: should we create a private IObjectContainer to set the update depth?
            lock(genreLock)
            {
                client.Store(g);
                client.Commit();
            }
        }

        public IQueryable<WritingStyle> GetWritingStyles ()
        {
            return client.AsQueryable<WritingStyle>();
        }
        public int AddWritingStyle (WritingStyle w)
        {
            throw new NotImplementedException();
        }
        public void RemoveWritingStyle (WritingStyle w)
        {
            throw new NotImplementedException();
        }
        public void UpdateWritingStyle (WritingStyle w)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BaseBeek> GetBeek ()
        {
            return client.AsQueryable<BaseBeek>();
        }
        public BaseBeek GetBeekById(int id)
        {
            return GetBeek().Where(b => b.Id.Equals(id)).FirstOrDefault();
        }

        public int AddBeek (BaseBeek beek)
        {
            lock (beekLock)
            {
                beek.Id = GetBeek().Select(b => b.Id).DefaultIfEmpty(-1).Max() + 1;
                client.Store(beek);
                client.Commit();
            }
            return beek.Id;
        }
        public void RemoveBeek (BaseBeek b)
        {
            lock (beekLock)
            {
                client.Delete(b);
                client.Commit();
            }
        }
        public void UpdateBeek (BaseBeek b)
        {
            lock (beekLock)
            {
                client.Store(b);
                client.Commit();
            }
        }

        private static string EnsureDb4OFilePath(string db4oFilePath)
        {
            if (!Path.IsPathRooted(db4oFilePath))
            {
                var appDataPath = (string)AppDomain.CurrentDomain.GetData("DataDirectory") ??
                                  AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                db4oFilePath = Path.Combine(appDataPath, db4oFilePath);
            }

            FileInfo file = new FileInfo(db4oFilePath);
            if (!file.Exists)
            {
                file.Directory.Create();
                file.Create();
            }
            return db4oFilePath;
        }
    }
}
