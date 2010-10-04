using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        public new HomeViewData ViewData { get { return (HomeViewData)base.ViewData; } set { base.ViewData = value; } }
        public HomeController ()
        {
            ViewData = new HomeViewData();
        }

        public ActionResult Index()
        {
            return View(ViewData);
        }

    }
}
