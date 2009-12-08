using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Linq;

namespace Boris.BeekProject.Model.Accounts
{
    public class User: IUser
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        private string HashedPassword { get; set; }
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsAnonymous { 
            get{
                return IsInRole("Anonymous");
            }
        }
        private IList<IRole> Roles { get; set; }

        public User()
        {
            Roles = new List<IRole>();
            Salt = GetSalt();
        }
        public User(string userName, string password, string email):this()
        {
            Name = userName;
            HashedPassword = GetHashedPassword(Salt, password);
            Email = email;
        }

        public bool IsInRole (IRole role)
        {
            return Roles.Contains(role);
        }
        public bool IsInRole (string roleName)
        {
            return Roles.Any(r => r.Equals(roleName));
        }
        public void AddRole(IRole role)
        {
            lock (Roles)
            {
                if (!IsInRole(role))
                {
                    Roles.Add(role);
                }
            }
        }
        public void AddRoles (IEnumerable<IRole> roles)
        {
            lock (Roles)
            {
                Roles = Roles.Union(roles).ToList();
            }
        }
        public void RemoveRole(IRole role)
        {
            lock (Roles)
            {
                Roles = Roles.Where(r => !r.Equals(role)).ToList();
            }
        }

        public bool Challenge(string password)
        {
            return HashedPassword.Equals(GetHashedPassword(Salt, password));
        }
        public IIdentity Identity { get; private set; }
        private static string GetSalt ()
        {
            return (new Random().Next() + DateTime.Now.Ticks).ToString();
        }
        private static string GetHashedPassword (string salt, string password)
        {
            //ToDo, md5 hash this bitch
            return string.Empty;
        }
    }
}
