using System;
using System.Collections.Generic;
using System.Linq;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.DataAccess.Memory
{
    public class MemoryUserRepository: IUserRepository
    {
        public Guid AddUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public void AddUsers(IEnumerable<User> newUsers)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IUser CreateAnonymousUser()
        {
            throw new NotImplementedException();
        }

        public string NonUserGeneratedPassword
        {
            get { throw new NotImplementedException(); }
        }
    }
}