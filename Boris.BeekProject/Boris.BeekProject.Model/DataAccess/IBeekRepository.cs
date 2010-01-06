using System;
using System.Linq;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface IBeekRepository
    {
        IQueryable<BaseGenre> GetGenres();
        Boolean AddGenre(BaseGenre g);
        Boolean RemoveGenre(BaseGenre g);
        Boolean UpdateGenre(BaseGenre g);

        IQueryable<WritingStyle> GetWritingStyles();
        Boolean AddWritingStyle(WritingStyle w);
        Boolean RemoveWritingStyle(WritingStyle w);
        Boolean UpdateWritingStyle(WritingStyle w);

        IQueryable<BaseBeek> GetBluePrints();
        IQueryable<BaseBeek> GetBeek();
        Boolean AddBeek(BaseBeek b);
        Boolean RemoveBeek(BaseBeek b);
        Boolean UpdateBeek(BaseBeek b);
    }
}
