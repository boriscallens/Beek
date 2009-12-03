using System;
using System.Linq;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface ICatalogRepository
    {
        IQueryable<Genre> GetGenres();
        Boolean AddGenre(Genre g);
        Boolean RemoveGenre(Genre g);

        IQueryable<WritingStyle> GetWritingStyles();
        Boolean AddWritingStyle(WritingStyle w);
        Boolean RemoveWritingStyle(WritingStyle w);

        IQueryable<BluePrint> GetBluePrints();

        IQueryable<BaseBeek> GetBeek();
        Boolean AddBeek(BaseBeek b);
        Boolean RemoveBeek(BaseBeek b);
    }
}
