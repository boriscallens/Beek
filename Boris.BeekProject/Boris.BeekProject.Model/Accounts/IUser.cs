using System;
using System.Security.Principal;

namespace Boris.BeekProject.Model.Accounts
{
    public interface IUser: IPrincipal
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Salt { get; set; }
        string Email { get; set; }
        DateTime CreationTime { get; set; }
        DateTime? LastLoginAttempt { get; set; }
        bool IsApproved { get; set; }
        bool IsLockedOut { get; set; }

        bool Challenge(string password);
    }
}
