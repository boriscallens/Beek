using System.Web.Mvc;
using System.Web.Routing;

namespace Boris.BeekProject.Guis.Shared
{
    public static class MvcRouteFactory
    {
        public static RouteCollection GetRoutes(RouteCollection routes, bool isDebug)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");

            routes.MapRoute(
                "Accounts",
                "Accounts/{userName}/{action}",
                new { controller = "Account", action = "Index" }
            );

            routes.MapRoute(
                "SearchBeekIndex",
                "Search/Beek/",
                new { controller = "Search", action = "Beek"}
            );
            routes.MapRoute(
                "SearchBeekById",
                "Search/Beek/{id}",
                new { controller = "Search", action = "BeekById" }
            );


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            if (isDebug)
            {
                RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            }
            return routes;
        }
    }
}