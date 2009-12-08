using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IBeekRepository beekRepository;
        
        public HomeController(IUserRepository userRepository, IBeekRepository beekrepository)
        {
            userRepository = userRepository;
            beekRepository = beekRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult About()
        //{
        //    return View();
        //}
    }
}
