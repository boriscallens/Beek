using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        public HomeController (IUserRepository userRepository): base(userRepository, new HomeViewModel())
        {
            viewModel.CurrentNavBlock = NavBlocks.Home;
        }

        public ActionResult Index()
        {
            return View(viewModel);
        }
    }
}
