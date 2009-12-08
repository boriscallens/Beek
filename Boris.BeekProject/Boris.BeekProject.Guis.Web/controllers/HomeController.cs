using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        public HomeController(){}
        public HomeController(IUserRepository repository) : base(repository){}

        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
