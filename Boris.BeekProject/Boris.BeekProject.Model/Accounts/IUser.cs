using System;
using System.Security.Principal;
using System.Collections.Generic;

namespace Boris.BeekProject.Model.Accounts
{
    public interface IUser: IPrincipal, IEquatable<IUser>
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Salt { get; set; }
        string Email { get; set; }
        DateTime CreationTime { get; set; }
        DateTime? LastLoginAttempt { get; set; }
        bool IsApproved { get; set; }
        bool IsLockedOut { get; set; }
        IEnumerable<Contributions> Contributions { get;}
        Sources Source { get; set; }
        bool IsDefault { get; set; }
        bool IsAnonymous { get; }
        string AvatarFileName { get; set; }

        bool Challenge(string password);
        bool IsContributingAs(Contributions contribution);
        void AddContribution(Contributions contribution);
        void AddContributions(IEnumerable<Contributions> contributions);
        void RemoveContribution(Contributions contribution);
        void SetPassword(string password);
    }
}
