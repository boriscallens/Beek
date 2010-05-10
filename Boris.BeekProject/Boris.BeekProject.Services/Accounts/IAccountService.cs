using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Services.Accounts
{
    public interface IAccountService
    {
        bool DoesUserExist(string name);
        IEnumerable<User> CreateUsersInBatch(IEnumerable<string> names, Roles role, Sources source);
    }
}
