using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model.Filters
{
    public static class UserFilters
    {
        public static IQueryable<User> ByUserName(this IQueryable<User> qry, string userName){
            return qry.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
