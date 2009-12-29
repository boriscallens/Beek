using System;
using System.Linq;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface IBeekRepository
    {
        IQueryable<Genre> GetGenres();
        Boolean AddGenre(Genre g);
        Boolean RemoveGenre(Genre g);
        Boolean UpdateGenre(Genre g);

        IQueryable<WritingStyle> GetWritingStyles();
        Boolean AddWritingStyle(WritingStyle w);
        Boolean RemoveWritingStyle(WritingStyle w);
        Boolean UpdateWritingStyle(WritingStyle w);

        IQueryable<IBeek> GetBluePrints();
        IQueryable<IBeek> GetBeek();
        Boolean AddBeek(IBeek b);
        Boolean RemoveBeek(IBeek b);
        Boolean UpdateBeek(IBeek b);
    }
}
