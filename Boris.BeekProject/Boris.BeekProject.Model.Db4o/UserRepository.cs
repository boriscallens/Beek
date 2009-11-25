using System;
using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Boris.BeekProject.Model.Db4o
{
    public class UserRepository: IUserRepository
    {
        private static IObjectServer server = Db4oFactory.OpenServer(
            ConfigurationManager.AppSettings["db4o.userRepository.path"], 0);
        private static IObjectContainer client = Db4oFactory.OpenClient("localhost", 0, string.Empty, string.Empty);

        public Guid AddUser(IUser user)
        {
            using (var db = GetDb())
            {
                Guid guid = new Guid();
                user.Id = guid;
                db.Store(user);
                return guid;
            }
        }

        public void RemoveUser(Guid id)
        {
            using (var db = GetDb())
            {
                Guid guid = new Guid();
                user.Id = guid;
                db.Store(user);
                return guid;
            }
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
    
        private IObjectContainer GetDb()
        {
            return Db4oFactory.OpenFile();
        }
    }
}
