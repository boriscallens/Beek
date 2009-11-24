using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model.Filters
{
    public static class BeekFilters
    {
        public static Beek WithId(this IQueryable<Beek> qry, int id) {
            return (from b in qry
                    where b.Id == id
                    select b).SingleOrDefault<Beek>();
        }
        public static IQueryable<Beek> WithNameContaining(this IQueryable<Beek> qry, string title) {
            return from b in qry
                   where b.Title.Contains(title)
                   select b;
        }
    }
}
