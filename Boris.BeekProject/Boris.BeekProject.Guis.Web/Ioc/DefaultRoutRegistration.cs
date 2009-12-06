using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Boris.BeekProject.Guis.Web.Ioc
{
    public class DefaultRoutRegistration: IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );
        }
    }
}