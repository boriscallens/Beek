using System;
using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class AccountController : BaseBeekController
    {
        private readonly IBeekRepository beekRepos;
        public new AccountViewData ViewData { get { return (AccountViewData)base.ViewData.Model; } set { base.ViewData = value; } }

        public AccountController(IUserRepository userRepository, IBeekRepository beekRepository) : base(userRepository)
        {
            ViewData = new AccountViewData {CurrentNavBlock = NavBlocks.MyStuff};
            beekRepos = beekRepository;
        }

        // GET: /accounts/register
        public ViewResult Register(Guid userId)
        {
            base.ViewData.User = UserRepository.GetUser(userId)
                ?? UserRepository.CreateAnonymousUser();
            if(base.ViewData.User.IsAnonymous)
            {
                base.ViewData.User.Name = string.Empty;
            }
            return View(ViewData);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(IUser user)
        {
            if (ModelState.IsValid)
            {
                user.RemoveRole(Roles.Anonymous);
                UserRepository.UpdateUser(user);
            }
            base.ViewData.User = user;
            SetUserCookie(user);
            return RedirectToAction("index", "home");
        }

        // GET: /accounts/login        
        public ActionResult LogIn()
        {
            throw new NotImplementedException();
            //return View();
        }
        // POST: /accounts/login
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogIn(string username, string password, string referer)
        {
            IUser user = UserRepository.GetUser(username);
            if (user == null || user.IsAnonymous)
            {
                base.ViewData.Messages.Add(MessageKeys.UserNameNotFound, String.Format("Couldn't find the username {0}, but feel free to register it!", username));
                base.ViewData.User.Name = username;
                return View("register", ViewData);
            }

            if(user.Challenge(password))
            {
                base.ViewData.User = user;
                SetUserCookie(user);
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
            // If the user is the same as the current user, show an editable view
            // else it will be the public profile
            if(user == User)
            {
                return View("EditProfile");
            }
            return View("Profile");
        }
        // GET: /accounts/logout
        public ActionResult LogOut()
        {
            base.ViewData.User = UserRepository.CreateAnonymousUser();
            SetUserCookie(base.ViewData.User);
            return RedirectToAction("index", "home");
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
