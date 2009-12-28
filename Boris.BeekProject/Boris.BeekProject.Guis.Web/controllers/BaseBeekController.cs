using System;
using System.Web;
using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    public class BaseBeekController : Controller
    {
        internal IUserRepository userRepository;

        public new IUser User { get; set; }

        protected BaseBeekController(IUserRepository repository)
        {
            this.userRepository = repository;
            // If the user is not logged in we will either restore him from the cookie, or create an anon one
            if (User == null)
            {
                // Can we get him back from a cookie?
                User = RestoreUser(Request.Cookies["user"]);
                // Create an anon one and set the cookie with the id and ip
                if(User == null)
                {
                    User = CreateAnonymousUser();
                    Response.Cookies.Add(CreateUserCookie(User, Request.UserHostAddress));
                    TempData["isFirstTimeVisitor"] = true;
                }
            }
        }

        private IUser RestoreUser(HttpCookie cookie)
        {
            if (cookie != null)
            {
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
        private IUser CreateAnonymousUser()
        {
            IUser user = new User("Anonymous", "Anonymous", string.Empty);
            user.Id = userRepository.AddUser(user);
            return user;
        }
        private static HttpCookie CreateUserCookie(IUser user, string ip)
        {
            HttpCookie cookie = new HttpCookie("user");
            cookie.Values["id"] = user.Id.ToString();
            cookie.Values["ip"] = ip;
            cookie.Expires = DateTime.UtcNow.AddYears(1);
            return cookie;
        }
    }
}
