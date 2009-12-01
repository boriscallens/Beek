using System;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Model.Db4o
{
    public class CatalogRepository: ICatalogRepository
    {
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

        public IQueryable<BluePrint> GetBluePrints()
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
    }
}
