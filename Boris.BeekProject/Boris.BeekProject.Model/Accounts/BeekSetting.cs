using System;
using System.Collections.Generic;

namespace Boris.BeekProject.Model.Accounts
{
    public class BeekSetting: ISetting
    {
        public Guid Id { get; set;}
        public Guid UserId { get; set;}
        public bool IsDefault { get; set;}
        public IList<IRole> Roles { get; set; }
        public bool IsAnonymous { get; set; }

        public BeekSetting()
        {
            Roles = new List<IRole>();
        }
        public BeekSetting(Guid userId, bool isDefault):this()
        {
            UserId = userId;
            IsDefault = isDefault;
        }
    }
}
