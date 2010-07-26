using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Boris.BeekProject.Guis.Shared.Ioc
{
    public class DefaultRoutRegistration : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");

            routes.MapRoute(
                "Accounts",
                "Accounts/{userName}/{action}",
                new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                "SearchBeek",
                "Search/Beek/{id}",
                new { controller = "Search", action = "Beek" }
            );


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
    }
}