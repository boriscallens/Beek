using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model.Filters
{
    public static class WritingStyleFilters
    {
        public static IQueryable<WritingStyle> WithNameContaining(this IQueryable<WritingStyle> qry, string name)
        {
            return from p in qry
                   where p.Name.Contains(name)
                   select p;
        }
        public static WritingStyle WithId(this IQueryable<WritingStyle> qry, int id)
        {
            return (from p in qry
                   where p.Id == id
                    select p).FirstOrDefault();
        }
    }
}
