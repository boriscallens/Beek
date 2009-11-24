using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeekProject.Model;
using BeekProject.Model.Filters;
using BeekProject.Model.DataAccess;

namespace BeekProject.Services
{
    public class CatalogService : ICatalogService{
        private ICatalogRepository rep = null;

        public CatalogService(ICatalogRepository repository)
        {
            rep = repository;
            if (repository == null)
            {
                throw new ArgumentException("Repository cannot be null");
            }
        }
        //Beek
        public PagedList<Beek> GetBeeksPage(int page, int size)
        {
            return (rep.GetBeek().ToPagedList<Beek>(page, size));
        }
        public Beek GetBeekById(int id)
        {
            return rep.GetBeek().WithId(id);
        }
        public List<Beek> GetBeekWithNameContaining(string name)
        {
            return rep.GetBeek().WithNameContaining(name).ToList();
        }
        public void AddBeek(Beek b)
        {
            if (GetBeekById(b.Id) == null) {
                rep.AddBeek(b);
            }
        }
        public void RemoveBeek(int id)
        {
            Beek b = GetBeekById(id);
            if ( b != null)
            {
                rep.RemoveBeek(b);
            }
        }
        //Genres
        public IList<Genre> GetGenres( ) {
            return rep.GetGenres().ToList<Genre>();
        }
        public IList<BluePrint> GetBluePrints()
        {
            return rep.GetBluePrints().ToList<BluePrint>();
        }
        public IList<WritingStyle> GetWritingStyles()
        {
            return rep.GetWritingStyles().ToList<WritingStyle>();
        }
        public IList<Genre> GetGenresByName(string name) {
            return rep.GetGenres().WithNameContaining(name).ToList<Genre>();
        }
        public Genre GetGenreById(int id)
        {
            return rep.GetGenres().WithId(id);
        }
        public void AddGenre(Genre g)
        {
            if (GetGenreById(g.Id) == null)
            {
                rep.AddGenre(g);
            }
        }
        public void RemoveGenre(int id)
        {
            Genre g = GetGenreById(id);
            if (g != null)
            {
                rep.RemoveGenre(g);
            }
        }
    }
}
