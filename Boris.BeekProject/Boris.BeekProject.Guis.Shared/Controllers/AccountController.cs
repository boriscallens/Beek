using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Boris.BeekProject.Guis.Shared.Attributes;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [Navigation(NavigationBlocks.MyStuff)]
    public class AccountController : BaseBeekController
    {
        private readonly IAccountService accountService;
        private readonly IBeekRepository beekRepos;
        //private readonly AccountViewData viewData = new AccountViewData();
        private new AccountViewData ViewData { get { return (AccountViewData)base.ViewData; } set { base.ViewData = value; } }

        public AccountController(IAccountService accountService, IBeekRepository beekRepository)
        {
            this.accountService = accountService;
            beekRepos = beekRepository;
            ViewData = new AccountViewData();
        }

        // GET: /accounts/register
        public ViewResult Register(Guid userId)
        {
            ViewData.User = accountService.GetUserOrAnonymousUser(userId);
            ViewData.ViewUser = Mapper.Map<IUser, ViewUser>(ViewData.User);

            if (ViewData.User.IsAnonymous)
            {
                ViewData.ViewUser.Name = string.Empty;
            }
            return View(ViewData);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(ViewUser viewUser)
        {
            /* Should this testing logic be somewhere else? 
             * Testing the equal passwords is really view related and the real duplicate username testing is already in a service*/
            if (accountService.DoesUserExist(viewUser.Name))
                ModelState.AddModelError("Name", string.Format("The username {0} is already in use.", viewUser.Name));
            if( !viewUser.ArePasswordsEqual())
                ModelState.AddModelError("Password", "The passwords do not match");
            if (!ModelState.IsValid)
                return View(ViewData);

            IUser user = accountService.GetUserOrAnonymousUser(new Guid(viewUser.Id));
            user.Name = viewUser.Name ?? user.Name;
            user.Email = viewUser.Email ?? user.Email;
            user.SetPassword(viewUser.Password);
            user.RemoveContribution(Contributions.Anonymous);

            accountService.UpdateUser(user);
            accountService.StartUserSession(user);
            ViewData.User = user;
            /* ToDo: find a way to do a redirect without loosing my cookies */
            return View("~/Views/home/index.aspx");
        }
        // POST: /accounts/login
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogIn(string username, string password, string referer)
        {
            IUser user = accountService.GetUserOrAnonymousUser(username);
            if (user.IsAnonymous)
            {
                ModelState.AddModelError("UserNameNotFound", String.Format("Couldn't find the username {0}, but feel free to register it!", username));
                ViewData.ViewUser = Mapper.Map<IUser, ViewUser>(user);
                ViewData.ViewUser.Name = username;
                return View("register", ViewData);
            }

            if(user.Challenge(password))
            {
                ViewData.User = user;
                accountService.StartUserSession(user);
                if(!string.IsNullOrEmpty(referer))
                {
                    // Don't know if this will work, but we essentially want to
                    // show the page the user was seeing before he was asked to log in
                    return View(referer, ViewData);
                }
                /* ToDo: find a way to do a redirect without loosing my cookies */
                return View("~/Views/home/index.aspx", ViewData);
            }
            // Oh noes! wrong password or username
            // Give them another try. Should we count the number of tries?
            return View(ViewData);
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
            ViewData.User = accountService.CreateAnonymousUser();
            /* ToDo: find a way to do a redirect without loosing my cookies */
            return View("~/Views/home/index.aspx");
        }
        // GET: /accounts/myBeek
        public ActionResult MyBeek()
        {
            // Any beek where the user is involved should be listed
            ViewData.Beek = beekRepos.GetBeek()
                .Where(b => b.Involvements.Any(
                    i => i.Key.Equals(ViewData.User)
                )
            );
            return View(ViewData);
        }
    }
}
