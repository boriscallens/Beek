using System;
using System.Linq;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using System.Threading.Tasks;

namespace Boris.BeekProject.Services.Accounts
{
    public class AccountService: IAccountService
    {
        private readonly IUserRepository userRepository;
        
        public AccountService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool DoesUserExist(string name)
        {
            return userRepository.GetUsers().Any(user => user.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<User> CreateUsersInBatch(IEnumerable<string> names, Roles role, Sources source)
        {
            var newUsers =
                names.Where(name => !DoesUserExist(name)).Select(
                    name => new User(name, userRepository.NonUserGeneratedPassword, string.Empty));
            Parallel.ForEach(newUsers, user =>
                                           {
                                               user.Source = source;
                                               user.AddRole(role);
                                           });
            userRepository.AddUsers(newUsers);
            return newUsers;
        }
    }
}
