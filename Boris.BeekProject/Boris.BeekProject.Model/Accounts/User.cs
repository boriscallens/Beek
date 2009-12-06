using System;
//using System.ComponentModel.DataAnnotations;

namespace Boris.BeekProject.Model.Accounts
{
    public class User
    {
        //[Required]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        private string Password { get; set; }
        public string Email { get; set; }

        public User() { }
        public User(string userName, string passWord, string email)
        {
            UserName = userName;
            Password = passWord;
            Email = email;
        }
    }
}
