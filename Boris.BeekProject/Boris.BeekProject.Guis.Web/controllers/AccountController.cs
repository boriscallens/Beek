using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
        public ActionResult Index(IUser user)
        {
            //If the user is the same as the current user, show an editable view
            //else it will be the public profile
            return View();
        }

    }
}
