using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boris.BeekProject.Model
{
    public interface IUserRepository
    {
        Guid AddUser(IUser);
        void RemoveUser(Guid id);
        IUser GetUserDataForUserName(string name, string brandCode);
        IUser GetUserDataForUserId(Guid id);
        T GetSetting<T>(Guid userId) where T : ISetting, new();
        void SetSetting(ISetting setting);
        IQueryable<T> GetDefaultSettings<T>(string brandCode) where T : ISetting, new();
    }
}
