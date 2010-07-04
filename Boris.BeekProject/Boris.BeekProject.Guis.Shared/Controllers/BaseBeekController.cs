using System;
using System.Web;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.Utils.Mvc.Attributes;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [Logging]
    public abstract class BaseBeekController : Controller
    {
        protected readonly IUserRepository UserRepository;
        protected new virtual BaseBeekViewData ViewData { get; private set; }

        protected BaseBeekController(IUserRepository repository)
        {
            UserRepository = repository;
        }
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If the user is not logged in we will either restore him from the cookie, or create an anon one
            if (ViewData.User == null)
            {
                // Can we get him back from a cookie?
                ViewData.User = RestoreUser(Request.Cookies["user"]);
                // Create an anon one and set the cookie with the id and ip
                if (ViewData.User == null)
                {
                    ViewData.User = UserRepository.CreateAnonymousUser();
                    SetUserCookie(ViewData.User);
                    ViewData.Messages.Add(MessageKeys.FirstTimeVisitor, "First time user? Check all the stuffs etc");
                }
            }
            if (ViewData.User.IsAnonymous)
            {
                ViewData.Messages.Add(MessageKeys.IsAnonymous, "You are currently not logged in.");
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetUserCookie(IUser user)
        {
            Response.Cookies.Set(CreateUserCookie(user, Request.UserHostAddress));
        }

        private IUser RestoreUser(HttpCookie cookie)
        {
            if (cookie != null)
            {
                try
                {
                    return UserRepository.GetUser(new Guid(cookie.Values["id"]));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
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