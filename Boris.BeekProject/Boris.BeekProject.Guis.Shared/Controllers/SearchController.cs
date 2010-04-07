using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Search;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class SearchController : BaseBeekController
    {
        private readonly ISearchService searchService;
        private SearchViewModel ViewModel { get { return viewModel as SearchViewModel; } }

        public SearchController(IUserRepository userRepository, ISearchService searchService)
            : base(userRepository, new SearchViewModel())
        {
            this.searchService = searchService;
            viewModel.CurrentNavBlock = NavBlocks.Search;
        }

        // GET: /Search
        public ActionResult Index()
        {
            return View("Search", viewModel);
        }

        // GET: /Beek/123456
        public ActionResult Index(int beekId)
        {
            return Search(new BeekSearchbag {BeekId = beekId}, 0, 1);
        }

        // GET: /Beek/Search?name=1984&author=george+orwell
        public ActionResult Search(BeekSearchbag bag, int skip, int take)
        {
            ViewModel.FoundBeek = searchService.SearchBeek(bag, skip, take);
            if (ViewModel.FoundBeek.Any())
            {
                return View(ViewModel);
            }
            // ToDo: return "noBeekFound" view
            return View("noBeekFound", ViewModel);
        }
    }
}
