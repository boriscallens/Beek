using System;
using System.Web.Mvc;

namespace Boris.Callens.BeekProject.Website.controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index(Guid userId)
        {

            return View();
        }

    }
}
