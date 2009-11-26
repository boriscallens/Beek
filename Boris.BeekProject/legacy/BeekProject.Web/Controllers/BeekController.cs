using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeekProject.Data;
using BeekProject.Services;
using BeekProject.Data.DataAccess;

namespace BeekProject.Web.Controllers
{
    public class BeeksViewData : ViewDataDictionary {
        public PagedList<Beek> Beeks { get; set; }
    }

    public class BeekController : Controller
    {
        private BeeksViewData viewData = new BeeksViewData();
        private CatalogService cs = new CatalogService(new SqlCatalogRepository());
        public ActionResult Index()
        {
            return(List(1));
        }
        public ActionResult List(int pageNr) {
            viewData.Beeks = cs.GetBeeksPage(pageNr, 40);
            return View("List", viewData);
        }
    }
}
