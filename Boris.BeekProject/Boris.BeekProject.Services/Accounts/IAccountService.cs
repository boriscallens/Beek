using System;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Services.Accounts
{
    public interface IAccountService
    {
        IEnumerable<IUser> CreateUsersInBatch(IEnumerable<string> names, Contributions contribution, Sources source);

        bool DoesUserExist(string name);
        IUser GetUser(string username);
        IUser GetUser(Guid userId);
        IUser GetUserOrAnonymousUser(string username);
        IUser GetUserOrAnonymousUser(Guid userId);
        IUser GetOrCreateUserWithContribution(string username, Contributions contribution);
        IUser CreateAnonymousUser();
        void UpdateUser(IUser user);

        void StartUserSession(IUser user);
        void EndUserSession();
        bool IsUserSessionActive(IUser user);
        IUser GetUserFromSesion();
    }
}
