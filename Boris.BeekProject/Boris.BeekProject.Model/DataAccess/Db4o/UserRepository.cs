using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Boris.BeekProject.Model.Accounts;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Boris.Utils.IO;

namespace Boris.BeekProject.Model.DataAccess.Db4o
{
    public class UserRepository: IUserRepository
    {
        private readonly IObjectServer server;
        private readonly IObjectContainer client;
        private readonly object userLock;

        public UserRepository(): this(IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["userRepository.path.db4o"])){}
        public UserRepository(string db4oFilePath)
        {
            FileInfo file = new FileInfo(db4oFilePath);
            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }
            server = Db4oFactory.OpenServer(db4oFilePath, 0);
            client = server.OpenClient();
            userLock = new object();
        }
        public UserRepository(IObjectServer server)
        {
            this.server = server;
            client = server.OpenClient();
            userLock = new object();
        }

        public void UpdateUser(IUser user)
        {
            lock(userLock)
            {
                client.Store(user);
                client.Commit();
            }
        }
        public IQueryable<IUser> GetUsers()
        {
            return client.Cast<IUser>().AsQueryable();
        }
        public Guid AddUser(IUser user)
        {
            lock (userLock)
            {
                user.Id = Guid.NewGuid();
                client.Store(user);
                client.Commit();
            }
            return user.Id;
        }
        public void RemoveUser(Guid id)
        {
            RemoveUser(GetUser(id));
        }
        public void RemoveUser(IUser user)
        {
            if(user == null)
            {
                return;
            }
            lock (userLock)
            {
                client.Delete(user);
                client.Commit();
            }
        }
        public IUser GetUser(Guid id)
        {
            return GetUsers().Where(u => u.Id.Equals(id)).SingleOrDefault();
        }
        public IUser GetUser(string name)
        {
            return GetUsers()
                .Where(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }

    }
}
