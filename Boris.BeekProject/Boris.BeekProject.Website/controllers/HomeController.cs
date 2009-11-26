using System.Web.Mvc;

namespace Boris.BeekProject.Website.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
