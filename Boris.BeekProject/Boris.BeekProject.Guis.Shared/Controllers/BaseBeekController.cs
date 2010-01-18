using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.Utils.Mvc.Attributes;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [Logging]
    public class BaseBeekController : Controller
    {
        protected readonly IUserRepository userRepository;
        protected readonly BaseBeekViewModel viewModel;

        protected BaseBeekController(IUserRepository repository, BaseBeekViewModel viewModel)
        {
            userRepository = repository;
            this.viewModel = viewModel;
            this.viewModel.Messages = new List<string>();
        }
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If the user is not logged in we will either restore him from the cookie, or create an anon one
            if (viewModel.User == null)
            {
                // Can we get him back from a cookie?
                viewModel.User = RestoreUser(Request.Cookies["user"]);
                // Create an anon one and set the cookie with the id and ip
                if (viewModel.User == null)
                {
                    viewModel.User = userRepository.CreateAnonymousUser();
                    Response.Cookies.Add(CreateUserCookie(viewModel.User, Request.UserHostAddress));
                    viewModel.Messages.Add("isFirstTimeVisitor");
                }
            }
            if (viewModel.User.IsAnonymous)
            {
                viewModel.Messages.Add("isAnonymous");
            }
            base.OnActionExecuting(filterContext);
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