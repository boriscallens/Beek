using System;
using System.Collections.Generic;
using System.Web;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Services.Accounts
{
    public interface IAccountService
    {
        IEnumerable<IUser> CreateUsersInBatch(IEnumerable<string> names, Roles role, Sources source);

        bool DoesUserExist(string name);
        IUser GetUser(string username);
        IUser GetUser(Guid userId);
        IUser GetUserOrAnonymousUser(string username);
        IUser GetUserOrAnonymousUser(Guid userId);
        IUser CreateAnonymousUser();
        void UpdateUser(IUser user);

        void StartUserSession(IUser user);
        void EndUserSession();
        bool IsUserSessionActive(IUser user);
        IUser GetUserFromSesion();
    }
}
