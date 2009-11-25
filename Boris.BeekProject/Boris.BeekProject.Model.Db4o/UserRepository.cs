using System;
using System.Linq;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Model.Db4o
{
    public class UserRepository: IUserRepository
    {
        public Guid AddUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IUser GetUserDataForUserName(string name)
        {
            throw new NotImplementedException();
        }

        public IUser GetUserDataForUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public T GetSetting<T>(Guid userId) where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public void SetSetting(ISetting setting)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetDefaultSettings<T>() where T : ISetting, new()
        {
            throw new NotImplementedException();
        }
    }
}
