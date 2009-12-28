using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    [HandleError]
    public class HomeController : BaseBeekController
    {
        private readonly IBeekRepository beekRepository;
        
        public HomeController(IUserRepository userRepository, IBeekRepository beekrepository): base(userRepository)
        {
            this.beekRepository = beekRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
