using System;
using System.Web.Mvc;
using AutoMapper;
using Boris.BeekProject.Guis.Shared.Attributes;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using System.Linq;
using BeekTypes = Boris.BeekProject.Model.Beek.BeekTypes;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [Navigation(NavigationBlocks.Beek)]
    public class BeekController : BaseBeekController
    {
        private readonly IBeekRepository beekRepository;
        private readonly BeekViewData viewData = new BeekViewData();

        public BeekController(IBeekRepository beekRepository) 
        {
            this.beekRepository = beekRepository;
        }

        // GET: /Beek/Details/5
        public ActionResult Details(int id)
        {
            viewData.Beek = Mapper.Map<BaseBeek, ViewBeek>(beekRepository.GetBeek().Where(b => b.Id == id).SingleOrDefault());
            return View(viewData);
        }

        // GET: /Beek/Create
        public ActionResult Create()
        {
            return View(viewData);
        } 
        // POST: /Beek/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ViewBeek beek)
        {
            if (!ModelState.IsValid)
                return View(viewData);

            BaseBeek newBeek = new BaseBeek((BeekTypes)Enum.Parse(typeof(BeekTypes), beek.Type.ToString()));
            newBeek.Title = beek.Title.Trim();
            newBeek.Isbn = (beek.Isbn??"").Trim().ToUpper();
            newBeek.IsFiction = beek.IsFiction;

            try
            {
                int id = beekRepository.AddBeek(newBeek);
                return RedirectToAction("Details", new {id});
            }
            catch(Exception)
            {
                return View(viewData);
            }
        }
        
        // GET: /Beek/Edit/5
        public ActionResult Edit(int id)
        {
            viewData.Beek = Mapper.Map<BaseBeek, ViewBeek>(beekRepository.GetBeek().Where(b => b.Id == id).SingleOrDefault());
            return View(viewData);
        }
        // POST: /Beek/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(BaseBeek updatedBeek)
        {
            try
            {
                beekRepository.UpdateBeek(updatedBeek);
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
            viewData.Beeks = beekRepository.GetBeek().OrderBy(b => b.DateCreated).Take(take)
                .Select(oriBeek => Mapper.Map<BaseBeek, ViewBeek>(oriBeek));
            return View("List", viewData);
        }

    }
}
