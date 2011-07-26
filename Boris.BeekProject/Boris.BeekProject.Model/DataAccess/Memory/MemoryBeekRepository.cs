using System.Collections.Generic;
using System.Linq;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Model.DataAccess.Memory
{
    public class MemoryBeekRepository: IBeekRepository
    {
        IList<BaseBeek> beeks = new List<BaseBeek>();

        public IQueryable<BaseGenre> GetGenres()
        {
            throw new System.NotImplementedException();
        }

        public int AddGenre(BaseGenre g)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveGenre(BaseGenre g)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGenre(BaseGenre g)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<WritingStyle> GetWritingStyles()
        {
            throw new System.NotImplementedException();
        }

        public int AddWritingStyle(WritingStyle w)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveWritingStyle(WritingStyle w)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateWritingStyle(WritingStyle w)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<BaseBeek> GetBeek()
        {
            throw new System.NotImplementedException();
        }

        public BaseBeek GetBeekById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int AddBeek(BaseBeek b)
        {
            beeks.Add(b);
            return beeks.Count();
        }

        public void RemoveBeek(BaseBeek b)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBeek(BaseBeek b)
        {
            throw new System.NotImplementedException();
        }
    }
}