using System.Linq;
using System.Web.Mvc;
using System.Collections.Specialized;
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

        private static BeekSearchbag GetBeekSearchBag(NameValueCollection queryString)
        {
            BeekSearchbag bag = new BeekSearchbag();
            if(queryString.AllKeys.Contains("isbn"))
            {
                bag.IsbnContains = queryString["isbn"];
            }
            return bag;
        }
    }
}
