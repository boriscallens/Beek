using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeekProject.Model
{
    public class User
    {
        private int Id { get; set; }
        public string UserName { get; set; }
        private string Password { get; set; }
        public string Email { get; set; }

        public User(){}
        public User(string userName, string passWord, string email)
        {
            UserName = userName;
            Password = passWord;
            Email = email;
        }
    }
}
