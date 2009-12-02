using System;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Website.controllers
{
    public class AccountController : Controller
    {
        private AccountViewModel viewModel = new AccountViewModel();
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Index(Guid userId)
        {
            viewModel = new AccountViewModel {User = userRepository.GetUser(userId)};
            return View(viewModel);
        }
    }
}
