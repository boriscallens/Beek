using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model.DataAccess
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

        IQueryable<Beek> GetBeek();
        Boolean AddBeek(Beek b);
        Boolean RemoveBeek(Beek b);
    }
}
