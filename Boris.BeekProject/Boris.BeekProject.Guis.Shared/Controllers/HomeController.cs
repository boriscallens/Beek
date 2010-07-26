using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        public new HomeViewData ViewData { get { return (HomeViewData)base.ViewData; } set { base.ViewData = value; } }
        public HomeController (IUserRepository userRepository): base(userRepository)
        {
            ViewData = new HomeViewData {CurrentNavBlock = NavBlocks.Home};
        }

        public ActionResult Index()
        {
            return View(ViewData);
        }

    }
}
