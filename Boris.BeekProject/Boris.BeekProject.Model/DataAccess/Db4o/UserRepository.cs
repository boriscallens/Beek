using System;
using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Accounts;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Boris.BeekProject.Model.DataAccess.Db4o
{
    public class UserRepository: IUserRepository
    {
        private static IObjectServer server;

        private static IObjectContainer client;

        public UserRepository(): this(ConfigurationManager.AppSettings["userRepository.path.db4o"]){}

        public UserRepository(string db4oFilePath)
        {
            server = Db4oFactory.OpenServer(db4oFilePath, 0);
            client = server.OpenClient();
        }

        public Guid AddUser(IUser user)
        {
            user.Id = new Guid();
            client.Store(user);
            return user.Id;
        }

        public void RemoveUser(Guid id)
        {
            IUser user = GetUser(id);
            if(user != null)
            {
                client.Delete(user);
            }
        }

        public IUser GetUser(Guid id)
        {
            return (from IUser u in client
                    where u.Id.Equals(id)
                    select u).SingleOrDefault();
        }

        public IUser GetUser(string name)
        {
            return (from IUser u in client
                    where u.Name.Equals(name)
                    select u).FirstOrDefault();
        }

        public T GetSettingForUser<T>(Guid userId) where T : ISetting, new()
        {
            return (from T s in client
                    where s.UserId.Equals(userId)
                    select s).FirstOrDefault();
        }

        public T GetSetting<T>(Guid settingId) where T : ISetting, new()
        {
            return (from T s in client
                    where s.Id.Equals(settingId)
                    select s).FirstOrDefault();
        }

        public void SetSetting(ISetting setting)
        {
            if(setting.Id == default(Guid))
            {
                setting.Id = new Guid();
            }

        }

        public void RemoveSetting<T>(Guid settingId) where T : ISetting, new()
        {
            var setting = GetSetting<T>(settingId);
            if(setting.Id == settingId)
            {
                client.Delete(setting);
            }
        }

        public IQueryable<T> GetDefaultSettings<T>() where T : ISetting, new()
        {
            return (from T s in client
                    where s.IsDefault
                    select s).AsQueryable();
        }
    }
}
