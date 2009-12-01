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
        private static readonly IObjectServer server = 
            Db4oFactory.OpenServer(ConfigurationManager.AppSettings["db4o.userRepository.path"], 0);
        private static readonly IObjectContainer client = server.OpenClient();

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
