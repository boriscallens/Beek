using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services.Search;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Guis.Shared.Attributes;
using Boris.BeekProject.Services.Search.SearchBags;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [Navigation(NavigationBlocks.Beek)]
    public class SearchController : BaseBeekController
    {
        private readonly ISearchService searchService;
        private new SearchViewData ViewData { get { return (SearchViewData)base.ViewData; } set { base.ViewData = value; } }

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
            ViewData = new SearchViewData();
        }

        // GET: /Search
        public ActionResult Index()
        {
            return View("Search", ViewData);
        }

        // GET: /Search/Beek
        public ActionResult Beek(BeekSearchbag searchbag)
        {
            ViewData.UsedBeekSearchBag = searchbag ?? new BeekSearchbag();
            ViewData.FoundBeek = searchService.SearchBeek(ViewData.UsedBeekSearchBag, 0, 20).ToList();
            
            return View("Beek", ViewData);
        }

        // POST: /Search/Beek + all kinds of things
        [HttpPost]
        public ActionResult ProcessBeek(BeekSearchbag searchBag)
        {
            return RedirectToAction("Beek", searchBag);
        }

        public JsonResult TitleNames(string term) //variable name dedicated by jquery UI lib
        {
            BeekSearchbag searchbag = new BeekSearchbag { BeekTitleContains = term.Trim() };
            IEnumerable<BaseBeek> beek = searchService.SearchBeek(searchbag);
            //No sensitive data is returned so we can safely allow GET: http://haacked.com/archive/2009/06/25/json-hijacking.aspx
            return Json(beek.Select(u => u.Title), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AuthorNames(string term) //variable name dedicated by jquery UI lib
        {
            UserSearchbag searchbag = new UserSearchbag{UserNameContains = term.Trim()};
            IEnumerable<IUser> users = searchService.SearchUsers(searchbag);
            //No sensitive data is returned so we can safely allow GET: http://haacked.com/archive/2009/06/25/json-hijacking.aspx
            return Json(users.Select(u=>u.Name), JsonRequestBehavior.AllowGet);
        }
    }
}
