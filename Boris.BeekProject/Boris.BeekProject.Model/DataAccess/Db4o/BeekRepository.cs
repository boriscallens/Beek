using System;
using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Db4objects.Db4o;

namespace Boris.BeekProject.Model.DataAccess.Db4o
{
    public class BeekRepository: IBeekRepository
    {
        private static IObjectServer server;
        private static IObjectContainer client;

        public BeekRepository(): this(ConfigurationManager.AppSettings["userRepository.path.db4o"]){}

        public BeekRepository(string db4oFilePath)
        {
            server = Db4oFactory.OpenServer(db4oFilePath, 0);
            client = server.OpenClient();
        }

        public IQueryable<Genre> GetGenres()
        { 
            throw new NotImplementedException();
        }

        public bool AddGenre(Genre g)
        {
            throw new NotImplementedException();
        }

        public bool RemoveGenre(Genre g)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGenre(Genre g)
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

        public IQueryable<IBeek> GetBluePrints()
        {
            throw new NotImplementedException();
        }

        public IQueryable<IBeek> GetBeek()
        {
            throw new NotImplementedException();
        }

        public bool AddBeek(IBeek b)
        {
            throw new NotImplementedException();
        }

        public bool RemoveBeek(IBeek b)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBeek(IBeek b)
        {
            throw new NotImplementedException();
        }
    }
}
