using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Search;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class SearchController : BaseBeekController
    {
        public new readonly SearchViewData ViewData = new SearchViewData { CurrentNavBlock = NavBlocks.Search };

        private readonly ISearchService searchService;

        public SearchController(IUserRepository userRepository, ISearchService searchService)
            : base(userRepository)
        {
            this.searchService = searchService;
        }

        // GET: /Search
        public ActionResult Index()
        {
            return View("Search", ViewData);
        }

        // GET: /Beek/123456
        public ActionResult Index(int beekId)
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
            // ToDo: return "noBeekFound" view
            return View("noBeekFound", ViewData);
        }
    }
}
