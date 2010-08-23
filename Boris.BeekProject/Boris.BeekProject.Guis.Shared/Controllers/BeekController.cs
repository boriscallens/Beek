using System.Web.Mvc;
using AutoMapper;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using System.Linq;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    public class BeekController : BaseBeekController
    {
        private readonly IBeekRepository beeks;
        public new BeekViewData ViewData { get { return (BeekViewData)base.ViewData; } set { base.ViewData = value; } }

        public BeekController(IUserRepository repository, IBeekRepository beekRepository) 
        {
            ViewData = new BeekViewData ();
            beeks = beekRepository;
        }

        // GET: /Beek/Details/5
        public ActionResult Details(int id)
        {
            ViewData.Beek = Mapper.Map<BaseBeek, BaseBeekModel>(beeks.GetBeek().Where(b => b.Id == id).SingleOrDefault());
            return View(ViewData);
        }

        // GET: /Beek/Create
        public ActionResult Create()
        {
            return View(ViewData);
        } 
        // POST: /Beek/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(BaseBeek beek)
        {
            try
            {
                int id = beeks.AddBeek(beek);
                return RedirectToAction("Details", new {id});
            }
            catch
            {
                return View(ViewData);
            }
        }
        
        // GET: /Beek/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData.Beek = Mapper.Map<BaseBeek, BaseBeekModel>(beeks.GetBeek().Where(b => b.Id == id).SingleOrDefault());
            return View(ViewData);
        }
        // POST: /Beek/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(BaseBeek updatedBeek)
        {
            try
            {
                beeks.UpdateBeek(updatedBeek);
                return RedirectToAction("Details", new{id = updatedBeek.Id});
            }
            catch
            {
                return View("Edit", new { id = updatedBeek.Id });
            }
        }
    
        // GET: /Beek/Latest/20
        public ActionResult Latest(int take)
        {
            if (take > 50)
            {
                take = 50;
            }
            ViewData.Beeks = beeks.GetBeek().OrderBy(b => b.DateCreated).Take(take)
                .Select(oriBeek => Mapper.Map<BaseBeek, BaseBeekModel>(oriBeek));
            return View("List", ViewData);
        }

    }
}
