using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Services.Accounts;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class HeaderController: BaseBeekController
    {
        private readonly IAccountService accountService;
        private readonly HeaderViewData viewData = new HeaderViewData();

        public HeaderController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        
        public ActionResult Header()
        {
            viewData.CurrentNavBlock = NavBlocks.MyStuff;
            viewData.User = accountService.GetUserFromSesion();
            if (viewData.User.IsAnonymous)
            {
                return View("HeaderAnonymous", viewData);
            }
            return View("HeaderNotAnonymous", viewData);
        }
    }
}