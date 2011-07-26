using System.Web.Mvc;
using MvcContrib;
using MvcContrib.Filters;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Guis.Web.ViewModels;

namespace Boris.BeekProject.Guis.Web.Controllers
{
    [PassParametersDuringRedirect]
    public class BeekController : Controller
    {
        private readonly IBeekRepository beekRepository;

        public BeekController(IBeekRepository beekRepository)
        {
            this.beekRepository = beekRepository;
        }

        public ActionResult Add(BeekViewModel newBeek)
        {
            return View(newBeek);
        }
        [HttpPost]
        public ActionResult AddSubmit(BeekViewModel newBeek)
        {
            beekRepository.AddBeek(MapBeek(newBeek));
            ViewData.Model = newBeek;
            return this.RedirectToAction(c => c.Add(newBeek));
        }

        private static BaseBeek MapBeek(BeekViewModel newBeek)
        {
            return new BaseBeek(BeekTypes.LongStory) {Title = newBeek.Title};
        }
    }
}
