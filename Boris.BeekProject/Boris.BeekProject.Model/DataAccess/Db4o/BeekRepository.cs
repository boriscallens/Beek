using System;
using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Db4objects.Db4o;
using System.IO;

namespace Boris.BeekProject.Model.DataAccess.Db4o
{
    public class BeekRepository: IBeekRepository
    {
        private static IObjectServer server;
        private static IObjectContainer client;

        public BeekRepository(): this(ConfigurationManager.AppSettings["beekRepository.path.db4o"]){}

        public BeekRepository(string db4oFilePath)
        {
            FileInfo file = new FileInfo(db4oFilePath);
            if(file.)
            server = Db4oFactory.OpenServer(db4oFilePath, 0);
            client = server.OpenClient();
        }

        public IQueryable<BaseGenre> GetGenres()
        { 
            throw new NotImplementedException();
        }

        public bool AddGenre(BaseGenre g)
        {
            throw new NotImplementedException();
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
