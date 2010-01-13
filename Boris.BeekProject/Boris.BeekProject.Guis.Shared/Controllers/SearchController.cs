using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Search;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class SearchController : BaseBeekController
    {
        private readonly ISearchService searchService;

        public SearchController(IUserRepository userRepository, ISearchService searchService):base(userRepository)
        {
            this.searchService = searchService;
        }

        //
        // GET: /Beek/123456
        public ActionResult Index(int beekId)
        {
            BeekSearchbag bag = new BeekSearchbag {BeekId = beekId};
            BaseBeek beek = searchService.SearchBeek(bag).FirstOrDefault();
            if ( beek != null )
            {
                return View(beek);
            }
            // ToDo: return "beekNotFound" view
            return View("beekNotFound");
        }

        //
        // GET: /Beek/Search?name=1984&author=george+orwell
        public ActionResult Search(BeekSearchbag bag, int skip, int take)
        {
            IEnumerable<BaseBeek> beek = searchService.SearchBeek(bag, skip, take);
            if(beek.Any())
            {
                return View(beek);
            }
            // ToDo: return "noBeekFound" view
            return View("noBeekFound");
        }
    }
}