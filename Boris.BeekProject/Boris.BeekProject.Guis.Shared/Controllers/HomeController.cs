using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;
using MvcTurbine.ComponentModel;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        // Remove once MvcTurbine bins are out
        public HomeController ():this(ServiceLocatorManager.Current.Resolve<IUserRepository>()){}
        public HomeController (IUserRepository userRepository): base(userRepository, new HomeViewModel()){}

        public ActionResult Index()
        {
            return View(viewModel);
        }
    }
}
