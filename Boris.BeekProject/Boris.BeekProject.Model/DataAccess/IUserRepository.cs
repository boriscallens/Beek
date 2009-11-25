using System;
using System.Linq;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface IUserRepository
    {
        Guid AddUser(IUser user);
        void RemoveUser(Guid id);
        IUser GetUserDataForUserName(string name);
        IUser GetUserDataForUserId(Guid id);
        T GetSetting<T>(Guid userId) where T : ISetting, new();
        void SetSetting(ISetting setting);
        IQueryable<T> GetDefaultSettings<T>() where T : ISetting, new();
    }
}
