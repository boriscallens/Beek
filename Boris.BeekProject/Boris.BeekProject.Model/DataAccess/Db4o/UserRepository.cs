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

    }
}
