using System.Web.Mvc;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToRoute(new {controller = "Home", action = "LayoutTest"});
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LayoutTest()
        {
            return View();
        }
    }
}
