using System.Web.Mvc;
using Boris.Utils.Mvc.Attributes;
using Boris.BeekProject.Services.Accounts;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [LoggingFilter]
    public abstract class BaseBeekController : Controller
    {
        public IAccountService AccountService { get; set; }
    }
}