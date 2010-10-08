using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.Attributes;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Search;

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
        public ActionResult Beek()
        {
            return View("Beek", ViewData);
        }

        // POST: /Search/Beek/123456
        public ActionResult BeekById(int beekId)
        {
            return Search(new BeekSearchbag {BeekId = beekId}, 0, 1);
        }

        // GET: /Beek/Search?name=1984&author=george+orwell
        public ActionResult Search(BeekSearchbag bag, int skip, int take)
        {
            ViewData.FoundBeek = searchService.SearchBeek(bag, skip, take);
            if (ViewData.FoundBeek.Any())
            {
                return View(ViewData);
            }
            return View("noBeekFound", ViewData);
        }
    }
}
