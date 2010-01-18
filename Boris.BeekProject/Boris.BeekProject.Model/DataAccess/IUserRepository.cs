using System;
using System.Linq;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface IUserRepository
    {
        Guid AddUser(IUser user);
        void RemoveUser(Guid id);
        void RemoveUser(IUser user);
        void UpdateUser(IUser user);
        IQueryable<IUser> GetUsers();
        IUser GetUser(string name);
        IUser GetUser(Guid id);
        IUser CreateAnonymousUser();
    }
}