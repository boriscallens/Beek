using System;
using System.Linq;
using System.Security.Principal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boris.BeekProject.Model.Accounts
{
    public class User: IUser
    {
        private IList<Contributions> contributions;
        private string HashedPassword { get; set; }

        [Required (ErrorMessage="Id required")]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mail is required")]
        public string Email { get; set; }
        public string Salt { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LastLoginAttempt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public IEnumerable<Contributions> Contributions { get{ return contributions;}}
        public Sources Source { get; set; }
        public bool IsDefault { get; set; }
        public string AvatarFileName { get; set; }

        public bool IsAnonymous { 
            get
            {
                return IsContributingAs(Accounts.Contributions.Anonymous);
            }
        }

        public User()
        {
            contributions = new List<Contributions>{Accounts.Contributions.Anonymous};
            Salt = GetSalt();
        }
        public User(string userName, string password, string email):this()
        {
            Name = userName;
            SetPassword(password);
            Email = email;
            contributions.Remove(Accounts.Contributions.Anonymous);
        }

        public bool IsContributingAs (Contributions contribution)
        {
            return contributions.Contains(contribution);
        }
        // The interface requires me to call it role
        public bool IsInRole (string roleName)
        {
            return contributions.Any(r => r.ToString().Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
        }
        public void AddContribution(Contributions contribution)
        {
            lock (contributions)
            {
                if (!IsContributingAs(contribution))
                {
                    contributions.Add(contribution);
                }
            }
        }
        public void AddContributions (IEnumerable<Contributions> newRoles)
        {
            lock (contributions)
            {
                contributions = contributions.Union(newRoles).ToList();
            }
        }
        public void RemoveContribution(Contributions contribution)
        {
            lock (contributions)
            {
                contributions = contributions.Where(r => !r.Equals(contribution)).ToList();
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
        public override string ToString()
        {
            return this.Name;
        }
    }
}
