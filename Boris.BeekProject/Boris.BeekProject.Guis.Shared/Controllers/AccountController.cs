using System;
using System.Web.Mvc;
using System.Linq;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Services.Accounts;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class AccountController : BaseBeekController
    {
        private readonly IAccountService accountService;
        private readonly IBeekRepository beekRepos;
        private readonly AccountViewData viewData = new AccountViewData();

        public AccountController(IAccountService accountService, IBeekRepository beekRepository)
        {
            this.accountService = accountService;
            beekRepos = beekRepository;
        }

        // GET: /accounts/register
        public ViewResult Register(Guid userId)
        {
            viewData.User = accountService.GetUserOrAnonymousUser(userId);
            if (viewData.User.IsAnonymous)
            {
                viewData.User.Name = string.Empty;
            }
            return View(viewData);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(IUser user)
        {
            if (ModelState.IsValid)
            {
                user.RemoveRole(Roles.Anonymous);
                accountService.UpdateUser(user);
            }
            viewData.User = user;
            accountService.StartUserSession(user);
            return RedirectToAction("index", "home");
        }
        // POST: /accounts/login
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogIn(string username, string password, string referer)
        {
            IUser user = accountService.GetUserOrAnonymousUser(username);
            if (user.IsAnonymous)
            {
                viewData.Messages.Add(MessageKeys.UserNameNotFound, String.Format("Couldn't find the username {0}, but feel free to register it!", username));
                viewData.User.Name = username;
                return View("register", viewData);
            }

            if(user.Challenge(password))
            {
                viewData.User = user;
                accountService.StartUserSession(user);
                if(!string.IsNullOrEmpty(referer))
                {
                    // Don't know if this will work, but we essentially want to
                    // show the page the user was seeing before he was asked to log in
                    return View(referer);
                }
                // ToDo: What view do we want the user to see if we don't have a referal?
                return RedirectToAction("index", "home");
            }
            // Oh noes! wrong password or username
            // Give them another try. Should we count the number of tries?
            return View();
        }
        // GET: /accounts/boris
        public ActionResult Profile(IUser user)
        {
            if (accountService.IsUserSessionActive(user))
            {
                return View("EditProfile");
            }
            return View("Profile");
        }
        // GET: /accounts/logout
        public ActionResult LogOut()
        {
            accountService.EndUserSession();
            viewData.User = accountService.CreateAnonymousUser();
            return RedirectToAction("index", "home");
        }
        // GET: /accounts/myBeek
        public ActionResult MyBeek()
        {
            // Any beek where the user is involved should be listed
            viewData.Beek = beekRepos.GetBeek()
                .Where(b => b.Involvements.Any(
                    i => i.Key.Equals(viewData.User)
                )
            );
            return View(ViewData);
        }
    }
}
