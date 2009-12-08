using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
//using Boris.Utils.Mvc.Attributes;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    //[Authenticate]
    public class BaseBeekController: Controller
    {
        internal IUserRepository userRepository;

        //public BaseBeekController(){}
        protected BaseBeekController(IUserRepository repository)
        {
            this.userRepository = repository;    
        }
        public new IUser User { get; set; }
    }
}
