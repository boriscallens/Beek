using System;
using System.Collections.Generic;

namespace Boris.BeekProject.Model.Accounts
{
    public interface ISetting
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        IList<IRole> Roles {get; set;}
        bool IsDefault { get; set; }
        bool IsAnonymous { get; set; }
    }
}
