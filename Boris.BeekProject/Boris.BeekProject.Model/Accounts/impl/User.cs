using System;
using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boris.BeekProject.Model.Accounts
{
    public class User: IUser
    {
        private IList<Roles> roles;
        private string HashedPassword { get; set; }

        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public IEnumerable<Roles> Roles { get{ return roles;}}
        public bool IsDefault { get; set; }
        public bool IsAnonymous { 
            get
            {
                return IsInRole("Anonymous");
            }
        }

        public User()
        {
            roles = new List<Roles>{Accounts.Roles.Anonymous};
            Salt = GetSalt();
        }
        public User(string userName, string password, string email):this()
        {
            Name = userName;
            SetPassword(password);
            Email = email;
            roles.Remove(Accounts.Roles.Anonymous);
        }

        public bool IsInRole (Roles role)
        {
            return roles.Contains(role);
        }
        public bool IsInRole (string roleName)
        {
            return roles.Any(r => r.ToString().Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
        }
        public void AddRole(Roles role)
        {
            lock (roles)
            {
                if (!IsInRole(role))
                {
                    roles.Add(role);
                }
            }
        }
        public void AddRoles (IEnumerable<Roles> newRoles)
        {
            lock (roles)
            {
                roles = roles.Union(newRoles).ToList();
            }
        }
        public void RemoveRole(Roles role)
        {
            lock (roles)
            {
                roles = roles.Where(r => !r.Equals(role)).ToList();
            }
        }
        public void SetPassword(string password)
        {
            HashedPassword = GetHashedPassword(Salt, password);
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
            return Utils.Security.Md5.GetMd5Hash(salt + password);
        }

        public bool Equals(IUser other)
        {
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
