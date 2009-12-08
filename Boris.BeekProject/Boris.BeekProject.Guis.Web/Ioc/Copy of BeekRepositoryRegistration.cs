using MvcTurbine.ComponentModel;
using Boris.BeekProject.Services;

namespace Boris.BeekProject.Guis.Web.Ioc
{
    public class SearchServiceRegistration: IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<ISearchService, SearchService>();
        }
    }
}
