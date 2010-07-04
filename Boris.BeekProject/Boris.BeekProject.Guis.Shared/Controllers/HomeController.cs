using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        public readonly new HomeViewData ViewData = new HomeViewData(){CurrentNavBlock = NavBlocks.Home};
 
        public HomeController (IUserRepository userRepository): base(userRepository){}

        public ActionResult Index()
        {
            return View(ViewData);
        }
    }
}
