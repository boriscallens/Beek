using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Boris.BeekProject.Model.Accounts;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Boris.Utils.IO;
using User=Boris.BeekProject.Model.Accounts.User;

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
                if(GetUser(user.Id) != null)
                {
                    throw new ArgumentException("User with this Id already exists", "user");
                }
                user.Id = Guid.NewGuid();
                client.Store(user);
                client.Commit();
            }
            return user.Id;
        }
        public void AddUsers(IEnumerable<User> newUsers)
        {
            if(newUsers.Any(user=>!user.Id.Equals(default(Guid))))
            {
                throw new ArgumentException("Users cannot have ids issigned to them yet", "newUsers");
            }

            lock (userLock)
            {
                Parallel.ForEach(newUsers, user => {
                     user.Id = Guid.NewGuid();
                     client.Store(user);
                     client.Commit();
                });
            }
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
            if(id == Guid.Empty)
            {
                return null;
            }
            return GetUsers().Where(u => u.Id.Equals(id)).SingleOrDefault();
        }
        public IUser GetUser(string name)
        {
            return GetUsers()
                .Where(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }
        public IUser CreateAnonymousUser()
        {
            IUser user = new User("Username", "Password", string.Empty);
            user.AddContribution(Contributions.Anonymous);
            user.Id = AddUser(user);
            return user;
        }

        public string NonUserGeneratedPassword
        {
            get { return "6DC1A8E6-55D9-405C-8B8F-C2D1D20FC508"; }
        }
    }
}
