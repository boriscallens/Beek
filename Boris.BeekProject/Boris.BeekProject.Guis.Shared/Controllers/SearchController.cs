using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Search;
using Boris.Utils.Mvc.Attributes;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class SearchController : BaseBeekController
    {
        private readonly ISearchService searchService;
        private readonly SearchViewData viewData = new SearchViewData();

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        // GET: /Search
        public ActionResult Index()
        {
            return View("Search", viewData);
        }

        // GET: /Search/Beek
        public ActionResult Beek()
        {
            return View("Beek", viewData);
        }

        // POST: /Search/Beek/123456
        public ActionResult Beek(int beekId)
        {
            return Search(new BeekSearchbag {BeekId = beekId}, 0, 1);
        }

        // GET: /Beek/Search?name=1984&author=george+orwell
        public ActionResult Search(BeekSearchbag bag, int skip, int take)
        {
            viewData.FoundBeek = searchService.SearchBeek(bag, skip, take);
            if (viewData.FoundBeek.Any())
            {
                return View(viewData);
            }
            return View("noBeekFound", viewData);
        }
    }
}
