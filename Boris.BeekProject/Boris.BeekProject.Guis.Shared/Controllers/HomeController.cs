using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {       
        public HomeController(IUserRepository userRepository): base(userRepository)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
