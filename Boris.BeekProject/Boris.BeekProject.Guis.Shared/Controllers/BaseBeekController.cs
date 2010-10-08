using System.Web.Mvc;
using Boris.Utils.Logging;
using Boris.BeekProject.Services.Accounts;
using Boris.Utils.Mvc.Filters;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [HandleError]
    [LoggingFilter]
    public abstract class BaseBeekController : Controller
    {
        public IAccountService AccountService { get; set; }
        public ILoggingService LoggerService { get; set; }
    }
}