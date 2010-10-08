using System.Web.Mvc;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Guis.Shared.Attributes;

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

        public ActionResult Header(NavigationBlocks navBlock)
        {
            viewData.CurrentNavBlock = navBlock;
            viewData.User = accountService.GetUserFromSesion();
            if (viewData.User.IsAnonymous)
            {
                return View("HeaderAnonymous", viewData);
            }
            return View("HeaderNotAnonymous", viewData);
        }
    }
}