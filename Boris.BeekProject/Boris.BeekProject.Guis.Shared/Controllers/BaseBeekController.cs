using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Services.Accounts;
using Boris.Utils.Mvc.Attributes;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [LoggingFilter]
    public abstract class BaseBeekController : Controller
    {
        public NavBlocks CurrentNavBlock { get; set; }
        public IAccountService AccountService { get; set; }
    }
}