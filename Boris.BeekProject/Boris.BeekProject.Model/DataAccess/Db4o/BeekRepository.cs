using System;
using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Db4objects.Db4o;
using System.IO;
using Db4objects.Db4o.Linq;

namespace Boris.BeekProject.Model.DataAccess.Db4o
{
    public class BeekRepository: IBeekRepository
    {
        private static IObjectServer server;
        private static IObjectContainer client;

        public BeekRepository(): this(ConfigurationManager.AppSettings["beekRepository.path.db4o"]){}

        public BeekRepository(string db4oFilePath)
        {
            if(db4oFilePath.StartsWith("\\"))
            {
                //db4oFilePath = Path.Combine(Application, db4oFilePath);
            }
            FileInfo file = new FileInfo(db4oFilePath);
            
            if(!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            server = Db4oFactory.OpenServer(db4oFilePath, 0);
            client = server.OpenClient();
        }

        public IQueryable<BaseGenre> GetGenres()
        {
            // ToDo: down the latest db4o and use client.AsQueryable() straight from the bottle
            return client.Cast<BaseGenre>().AsQueryable();
        }

        public bool AddGenre(BaseGenre g)
        {
            client.Store(g);
            client.Commit();
            return true;
        }

        public bool RemoveGenre(BaseGenre g)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGenre(BaseGenre g)
        {
            throw new NotImplementedException();
        }

        public IQueryable<WritingStyle> GetWritingStyles()
        {
            throw new NotImplementedException();
        }

        public bool AddWritingStyle(WritingStyle w)
        {
            throw new NotImplementedException();
        }

        public bool RemoveWritingStyle(WritingStyle w)
        {
            throw new NotImplementedException();
        }

        public bool UpdateWritingStyle(WritingStyle w)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BaseBeek> GetBluePrints()
        {
            throw new NotImplementedException();
        }

        public IQueryable<BaseBeek> GetBeek()
        {
            throw new NotImplementedException();
        }

        public bool AddBeek(BaseBeek b)
        {
            throw new NotImplementedException();
        }

        public bool RemoveBeek(BaseBeek b)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBeek(BaseBeek b)
        {
            throw new NotImplementedException();
        }
    }
}
