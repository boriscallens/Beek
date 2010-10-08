using System.Web.Routing;
using MvcTurbine.Routing;
namespace Boris.BeekProject.Guis.Shared.Ioc
{
    public class DefaultRoutRegistration : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes = MvcRouteFactory.GetRoutes(routes, false);
        }
    }
}