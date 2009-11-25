using System;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model;

namespace Boris.BeekProject.Website.controllers
{
    public class AccountController : Controller
    {
        private AccountViewModel viewModel = new AccountViewModel();
        private IUserRepository userRepository = new UserRepository();
        
        public ActionResult Index(Guid userId)
        {
            viewModel = new AccountViewModel(){ User = }
            return View();
        }

    }
}
