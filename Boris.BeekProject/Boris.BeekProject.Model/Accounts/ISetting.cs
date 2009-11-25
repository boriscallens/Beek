using System;

namespace Boris.BeekProject.Model.Accounts
{
    public interface ISetting
    {
        int? Id { get; set; }
        Guid UserId { get; set; }
        bool IsDefaultUser { get; set; }
    }
}
