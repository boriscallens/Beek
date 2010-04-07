using System;
using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class AccountController : BaseBeekController
    {
        private readonly IBeekRepository beekRepos;
        private AccountViewModel ViewModel { get { return (AccountViewModel) viewModel; } }

        public AccountController(IUserRepository userRepository, IBeekRepository beekRepository) : base(userRepository, new AccountViewModel())
        {
            viewModel.CurrentNavBlock = NavBlocks.MyStuff;
            beekRepos = beekRepository;
        }

        // GET: /accounts/register
        public ViewResult Register(Guid userId)
        {
            viewModel.User = userRepository.GetUser(userId)
                ?? userRepository.CreateAnonymousUser();
            if(viewModel.User.IsAnonymous)
            {
                viewModel.User.Name = string.Empty;
            }
            return View(viewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(IUser user)
        {
            if (ModelState.IsValid)
            {
                user.RemoveRole(Roles.Anonymous);
                userRepository.UpdateUser(user);
            }
            viewModel.User = user;
            SetUserCookie(user);
            return RedirectToAction("index", "home");
        }

        // GET: /accounts/login        
        public ActionResult LogIn()
        {
            return View();
        }
        // POST: /accounts/login
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogIn(string username, string password, string referer)
        {
            IUser user = userRepository.GetUser(username);
            if (user == null || user.IsAnonymous)
            {
                viewModel.Messages.Add(MessageKeys.UserNameNotFound, String.Format("Couldn't find the username {0}, but feel free to register it!", username));
                viewModel.User.Name = username;
                return View("register", viewModel);
            }

            if(user.Challenge(password))
            {
                viewModel.User = user;
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
            viewModel.User = userRepository.CreateAnonymousUser();
            SetUserCookie(viewModel.User);
            return RedirectToAction("index", "home");
        }
        // GET: /accounts/myBeek
        public ActionResult MyBeek()
        {
            // Any beek where the user is involved should be listed
            ViewModel.Beek = beekRepos.GetBeek()
                .Where(b => b.Involvements.Any(
                    i => i.Key.Equals(viewModel.User)
                )
            );
            return View(ViewModel);
        }
    }
}
