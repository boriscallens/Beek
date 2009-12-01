using System;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.Db4o;

namespace Boris.BeekProject.Website.controllers
{
    public class AccountController : Controller
    {
        private AccountViewModel viewModel = new AccountViewModel();
        private readonly IUserRepository userRepository = new UserRepository();
        

        public ActionResult Index(Guid userId)
        {
            viewModel = new AccountViewModel {User = userRepository.GetUser(userId)};
            return View(viewModel);
        }

    }
}
