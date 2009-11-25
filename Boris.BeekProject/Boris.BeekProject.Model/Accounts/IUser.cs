using System;

namespace Boris.BeekProject.Model
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
        string BrandCode3 { get; set; }
        string Email { get; set; }
        DateTime CreationTime { get; set; }
        DateTime? LastLoginAttempt { get; set; }
        bool IsApproved { get; set; }
        bool IsLockedOut { get; set; }

        bool Challenge(string password);
    }
}
