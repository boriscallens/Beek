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
        IList<IRole> Roles { get; set; }
        bool IsDefault { get; set; }
        bool IsAnonymous { get;}

        bool Challenge(string password);
        bool IsInRole(IRole role);
        void AddRole(IRole role);
        void AddRoles(IEnumerable<IRole> roles);
        void RemoveRole(IRole role);
    }
}
