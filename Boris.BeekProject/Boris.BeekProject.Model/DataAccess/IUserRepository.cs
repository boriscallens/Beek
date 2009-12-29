using System;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.DataAccess
{
    public interface IUserRepository
    {
        Guid AddUser(IUser user);
        void RemoveUser(Guid id);
        IUser GetUser(string name);
        IUser GetUser(Guid id);
    }
}