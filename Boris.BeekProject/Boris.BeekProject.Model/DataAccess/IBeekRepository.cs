using System;
using System.Linq;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface IBeekRepository
    {
        IQueryable<BaseGenre> GetGenres();
        int AddGenre(BaseGenre g);
        void RemoveGenre(BaseGenre g);
        void UpdateGenre(BaseGenre g);

        IQueryable<WritingStyle> GetWritingStyles();
        int AddWritingStyle(WritingStyle w);
        void RemoveWritingStyle(WritingStyle w);
        void UpdateWritingStyle(WritingStyle w);

        IQueryable<BaseBeek> GetBeek();
        int AddBeek(BaseBeek b);
        void RemoveBeek(BaseBeek b);
        void UpdateBeek(BaseBeek b);
    }
}
