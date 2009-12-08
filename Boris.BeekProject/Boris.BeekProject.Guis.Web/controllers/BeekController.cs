using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.SearchBags;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    public class BeekController : Controller
    {
        private readonly ISearchService searchService;

        public BeekController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        //
        // GET: /Beek/123456
        public ActionResult Index(int beekId)
        {
            BeekSearchbag bag = new BeekSearchbag() {BeekId = beekId};
            IBeek beek = searchService.SearchBeek(bag).FirstOrDefault();
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
            IEnumerable<IBeek> beek = searchService.SearchBeek(bag, skip, take);
            if(beek.Any())
            {
                return View(beek);
            }
            // ToDo: return "noBeekFound" view
            return View("noBeekFound");
        }
    }
}
