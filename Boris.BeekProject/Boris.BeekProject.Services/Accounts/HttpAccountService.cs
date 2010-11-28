using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using System.Threading.Tasks;
using Boris.Utils.Security;

namespace Boris.BeekProject.Services.Accounts
{
    public class HttpAccountService: IAccountService
    {
        private const string salt = "httpAccountServiceIsSalty!";
        private readonly IUserRepository userRepository;
        private readonly HttpContext context;
        
        public HttpAccountService(IUserRepository userRepository, HttpContext context)
        {
            this.userRepository = userRepository;
            this.context = context;
        }

        public IEnumerable<IUser> CreateUsersInBatch(IEnumerable<string> names, Contributions contribution, Sources source)
        {
            var newUsers =
                names.Where(name => !DoesUserExist(name)).Select(
                    name => new User(name, userRepository.NonUserGeneratedPassword, string.Empty));
            Parallel.ForEach(newUsers, user =>
                                           {
                                               user.Source = source;
                                               user.AddContribution(contribution);
                                           });
            userRepository.AddUsers(newUsers);
            return newUsers;
        }



        public IUser CreateAnonymousUser()
        {
            return userRepository.CreateAnonymousUser();
        }
        public void UpdateUser(IUser user)
        {
            userRepository.UpdateUser(user);
        }
        public IUser GetUser(string username)
        {
            return userRepository.GetUser(username);
        }
        public IUser GetUser(Guid userId)
        {
            return userRepository.GetUser(userId);
        }
        public IUser GetUserOrAnonymousUser(string username)
        {
            return GetUser(username) ?? CreateAnonymousUser();
        }
        public IUser GetUserOrAnonymousUser(Guid userId)
        {
            return GetUser(userId) ?? CreateAnonymousUser();
        }
        public IUser GetOrCreateUserWithContribution(string username, Contributions contribution)
        {
            IUser user = GetUser(username);
            if(user == null)
            {
                user = CreateAnonymousUser();
                user.Name = username;
            }
            user.AddContribution(contribution);
            userRepository.UpdateUser(user);
            return user;
        }
        public bool DoesUserExist(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && userRepository.GetUsers().Any(user => user.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public void StartUserSession(IUser user)
        {
            var response = context.Response;
            var request = context.Request;
            var cookie = CreateUserCookie(user, request.ServerVariables["REMOTE_ADDR"]);
            response.Cookies.Add(cookie);
            response.Flush();
        }
        public void EndUserSession()
        {
            StartUserSession(CreateAnonymousUser());
        }
        public bool IsUserSessionActive(IUser user)
        {
            var request = context.Request;
            var userCookie = request.Cookies["user"];
            return IsCookieValidFor(userCookie, user, request.UserHostAddress);
        }
        public IUser GetUserFromSesion()
        {
            var request = context.Request;
            var userCookie = request.Cookies["user"];
            var user = RestoreUserFromCookie(userCookie);
            if (user == null)
             {
                user = CreateAnonymousUser();
                StartUserSession(user);
            }
            return user;
        }

        private static HttpCookie CreateUserCookie(IUser user, string ip)
        {
            HttpCookie cookie = new HttpCookie("user");
            cookie.Values["id"] = user.Id.ToString();
            cookie.Values["ip"] = ip;
            cookie.Values["key"] = GetCookieHash(user.Id.ToString(), ip);
            cookie.Expires = DateTime.UtcNow.AddYears(1);
            cookie.Secure = true;
            cookie.HttpOnly = true;
            return cookie;
        }
        private static bool IsCookieValidFor(HttpCookie cookie, IUser user, string ip)
        {
            string expected = GetCookieHash(user.Id.ToString(), ip);
            string actual = cookie.Values["key"];
            return expected.Equals(actual);
        }
        private IUser RestoreUserFromCookie(HttpCookie cookie)
        {
            if (cookie != null) {
                try
                {
                    return userRepository.GetUser(new Guid(cookie.Values["id"]));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        private static string GetCookieHash(string userId, string ip)
        {
            return Md5.GetMd5Hash(salt + userId + ip);
        }
    }
}
