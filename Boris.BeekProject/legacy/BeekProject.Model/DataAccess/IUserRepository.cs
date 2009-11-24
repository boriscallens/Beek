using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        bool LoginUser(string userName, string passWord);
        bool RegisterUser(string userName, string passWord, string email);
        bool UnregisterUser(string userName);
    }
}
