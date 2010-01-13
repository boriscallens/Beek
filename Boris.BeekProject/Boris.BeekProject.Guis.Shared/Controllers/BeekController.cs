using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using System.Linq;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class BeekController : BaseBeekController
    {
        private readonly IBeekRepository beeks;
        private readonly BeekViewModel viewModel;

        protected BeekController(IUserRepository repository, IBeekRepository beekRepository) 
            : base(repository, new BeekViewModel() )
        {
            beeks = beekRepository;
            viewModel = new BeekViewModel();
        }

        //
        // GET: /Beek/Details/5
        public ActionResult Details(int id)
        {
            viewModel.Beek = beeks.GetBeek().Where(b => b.Id == id).SingleOrDefault();
            return View(viewModel);
        }

        //
        // GET: /Beek/Create
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Beek/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(BaseBeek beek)
        {
            try
            {
                beeks.AddBeek(beek);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Beek/Edit/5
        public ActionResult Edit(int id)
        {
            viewModel.Beek = beeks.GetBeek().Where(b => b.Id == id).SingleOrDefault();
            return View();
        }

        //
        // POST: /Beek/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(BaseBeek updatedBeek)
        {
            try
            {
                beeks.UpdateBeek(updatedBeek);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
