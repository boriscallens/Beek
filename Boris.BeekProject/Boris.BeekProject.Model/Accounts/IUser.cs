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
        IEnumerable<Roles> Roles { get;}
        Sources Source { get; set; }
        bool IsDefault { get; set; }
        bool IsAnonymous { get; }

        bool Challenge(string password);
        bool IsInRole(Roles role);
        void AddRole(Roles role);
        void AddRoles(IEnumerable<Roles> roles);
        void RemoveRole(Roles role);
        void SetPassword(string password);
    }
}
