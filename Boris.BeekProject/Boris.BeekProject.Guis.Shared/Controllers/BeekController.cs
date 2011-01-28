using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Guis.Shared.ViewData;
using Boris.BeekProject.Guis.Shared.Attributes;
using Boris.BeekProject.Guis.Shared.ViewModels;
using BeekTypes = Boris.BeekProject.Model.Beek.BeekTypes;

namespace Boris.BeekProject.Guis.Shared.Controllers
{
    [Navigation(NavigationBlocks.Beek)]
    public class BeekController : BaseBeekController
    {
        private readonly IBeekRepository beekRepository;
        private readonly IAccountService accountService;

        private new BeekViewData ViewData { get { return (BeekViewData)base.ViewData; } set { base.ViewData = value; } }

        public BeekController(IBeekRepository beekRepository, IAccountService accountService) 
        {
            this.beekRepository = beekRepository;
            this.accountService = accountService;
            ViewData = new BeekViewData();
        }

        // GET: /Beek/Details/5
        public ActionResult Details(int id)
        {
            ViewData.Beek = Mapper.Map<BaseBeek, ViewBeek>(beekRepository.GetBeekById(id));
            return View(ViewData);
        }

        // GET: /Beek/Create
        public ActionResult Create()
        {
            return View(ViewData);
        } 
        // POST: /Beek/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ViewBeek beek)
        {
            if (!ModelState.IsValid)
                return View(ViewData);

            BaseBeek newBeek = new BaseBeek((BeekTypes)Enum.Parse(typeof(BeekTypes), beek.Type.ToString()));
            newBeek.Title = beek.Title.Trim();
            newBeek.Isbn = (beek.Isbn??"").Trim().ToUpper();
            newBeek.IsFiction = beek.IsFiction;

            if (!string.IsNullOrEmpty(beek.Author))
            {
                IUser author = accountService.GetOrCreateUserWithContribution(beek.Author, Contributions.Writer);
                newBeek.InvolveUser(author, Contributions.Writer);
            }

            try
            {
                int id = beekRepository.AddBeek(newBeek);
                return RedirectToAction("Details", new {id});
            }
            catch(Exception)
            {
                return View(ViewData);
            }
        }
        
        // GET: /Beek/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData.Beek = Mapper.Map<BaseBeek, ViewBeek>(beekRepository.GetBeek().Where(b => b.Id == id).SingleOrDefault());
            return View(ViewData);
        }
        // POST: /Beek/Edit/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(ViewBeek beek)
        {
            if(beek.Id.HasValue)
            {
                BaseBeek originalBeek = beekRepository.GetBeekById(beek.Id.Value);
                originalBeek.Title = beek.Title;
                //BaseBeek updatedBeek = Mapper.Map<ViewBeek, BaseBeek>(beek);
                beekRepository.UpdateBeek(originalBeek);
                return RedirectToAction("Details", new {id = beek.Id});
            }
            throw new ArgumentException("Couldn't find beek");
        }
    
        // GET: /Beek/Latest/20
        public ActionResult Latest(int take)
        {
            if (take > 50)
            {
                take = 50;
            }
            ViewData.Beeks = beekRepository.GetBeek().OrderBy(b => b.DateCreated).Take(take)
                .Select(oriBeek => Mapper.Map<BaseBeek, ViewBeek>(oriBeek));
            return View("List", ViewData);
        }
    
        // GET: /Beek/Thumb/20
        public ActionResult Thumb(int id)
        {
            ViewData.Beek = Mapper.Map<BaseBeek, ViewBeek>(beekRepository.GetBeekById(id));
            ViewData.Beek.CoverArtPath = Url.Content("~/content/pics/placeholders/coverArt.png");
            return View(ViewData);
        }
    }
}
