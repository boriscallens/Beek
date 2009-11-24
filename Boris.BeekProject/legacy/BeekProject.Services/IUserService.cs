using System;
using BeekProject.Model;
using System.Collections.Generic;

namespace BeekProject.Services
{
    public interface IUserService
    {
        IList<User> GetUsers();
        bool RegisterUser(string userName, string passWord, string email);
        User GetUserByUserName(string p);
        bool UnregisterUser(string usernam);
        bool LoginUser(string userName, string passWord);
    }
}
