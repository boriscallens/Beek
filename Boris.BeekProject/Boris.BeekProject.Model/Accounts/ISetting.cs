using System;

namespace Boris.BeekProject.Model.Accounts
{
    public interface ISetting
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        bool IsDefault { get; set; }
    }
}
