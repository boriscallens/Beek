using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model.Filters
{
    public static class GenreFilters
    {
        public static IQueryable<Genre> WithNameContaining(this IQueryable<Genre> qry, string name) {
            return from p in qry
                   where p.Name.Contains(name)
                   select p;
        }
        public static Genre WithId(this IQueryable<Genre> qry, int id) {
            return (from p in qry
                   where p.Id == id
                    select p).FirstOrDefault();
        }
    }
}
