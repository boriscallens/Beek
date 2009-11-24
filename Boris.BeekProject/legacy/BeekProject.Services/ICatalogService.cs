using System;
using System.Collections.Generic;
using BeekProject.Model;

namespace BeekProject.Services
{
    interface ICatalogService
    {
        void AddBeek(Beek b);
        void AddGenre(Genre g);
        Beek GetBeekById(int id);
        PagedList<Beek> GetBeeksPage(int page, int size);
        List<Beek> GetBeekWithNameContaining(string name);
        IList<BluePrint> GetBluePrints();
        Genre GetGenreById(int id);
        IList<Genre> GetGenres();
        IList<Genre> GetGenresByName(string name);
        IList<WritingStyle> GetWritingStyles();
        void RemoveBeek(int id);
        void RemoveGenre(int id);
    }
}
