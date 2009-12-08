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

        IQueryable<WritingStyle> GetWritingStyles();
        Boolean AddWritingStyle(WritingStyle w);
        Boolean RemoveWritingStyle(WritingStyle w);

        IQueryable<IBeek> GetBluePrints();
        IQueryable<IBeek> GetBeek();
        Boolean AddBeek(IBeek b);
        Boolean RemoveBeek(IBeek b);
    }
}
