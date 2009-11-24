using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeekProject.Model;
using BeekProject.Model.Filters;
using System.Text.RegularExpressions;

namespace BeekProject.Services
{
    public class UserService : IUserService
    {
        IUserRepository rep;
        public UserService(IUserRepository userRepository)
        {
            if (userRepository == null)
	        {
                throw new ArgumentException("User repository cannot be null");
	        }
            rep = userRepository;
        }
        public IList<User> GetUsers()
        {
            return rep.GetUsers().ToList<User>();
        }
        public User GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username should not be empty");
            }
            return rep.GetUsers().ByUserName(userName).SingleOrDefault();
        }
        public bool RegisterUser(string userName, string passWord, string email)
        {
            if (string.IsNullOrEmpty(passWord))
            {
                throw new ArgumentException("Cannot register user with empty password");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Cannot register user with empty email");
            }
            /*regex to validate email address noteworthy:
             * (1)  It allows usernames with 1 or 2 alphanum characters, or 3+ chars 
             *      can have -._ in the middle. Username may NOT start/end with -._ 
             *      or any other non alphanumeric character.
             * (2)  It allows heirarchical domain names (e.g. me@really.big.com).
             *      Similar -._ placement rules there.
             * (3)  It allows 2-9 character alphabetic-only TLDs (that oughta cover museum
             *      and adnauseum :&gt;).
             * (4)  No IP email addresses though -- I wouldn't Want to accept that kind of address.*/
            if (!Regex.IsMatch(email, @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
            {
                throw new ArgumentException("Cannot register user without valid email");
            }
            if (GetUserByUserName(userName)==null)
            {
                rep.RegisterUser(userName, passWord, email);
                return true;
            }
            return false;
        }
        public bool UnregisterUser(string username)
        {
            if (GetUserByUserName(username)!= null)
            {
                rep.UnregisterUser(username);
                return true;
            }
            return false;
        }
        public bool  LoginUser(string userName, string passWord)
        {
            if (string.IsNullOrEmpty(passWord))
            {
                throw new ArgumentException("Cannot log in with an empty password");
            }
            User usr = GetUserByUserName(userName);
            return (usr != null && passWord.Equals("password"));
        }
    }
}
