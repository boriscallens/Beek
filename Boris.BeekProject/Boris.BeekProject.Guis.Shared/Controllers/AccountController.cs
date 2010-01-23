using System;
using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Guis.Shared.ViewModels;
using MvcTurbine.ComponentModel;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class AccountController : BaseBeekController
    {
        // ToDo: remove when mvcTurbine 3.5bits are out
        public AccountController():this(ServiceLocatorManager.Current.Resolve<IUserRepository>()){}

        public AccountController(IUserRepository userRepository) : base(userRepository, new AccountViewModel()){}

        // GET: /accounts/register
        public ViewResult Register(Guid userId)
        {
            viewModel.User = userRepository.GetUser(userId) 
                ?? userRepository.CreateAnonymousUser();
            return View(viewModel);
        }
        // POST: /accounts/register

        // again waiting for mvcTurbine
        //public ActionResult Register(IUser user, string password)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        user.RemoveRole(Roles.Anonymous);
        //        userRepository.UpdateUser(user);
        //    }
        //    viewModel.User = user;
        //    return View(viewModel);
        //}
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                user.RemoveRole(Roles.Anonymous);
                userRepository.UpdateUser(user);
            }
            viewModel.User = user;
            return View(viewModel);
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
            if (user != null && user.Challenge(password))
            {
                // ToDo: Set user to session stuff
                if(!string.IsNullOrEmpty(referer))
                {
                    // Don't know if this will work, but we essentially want to
                    // show the page the user was seeing before he was asked to log in
                    return View(referer);
                }
                // ToDo: What view do we want the user to see if we don't have a referal?
                return View();
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
    }
}
