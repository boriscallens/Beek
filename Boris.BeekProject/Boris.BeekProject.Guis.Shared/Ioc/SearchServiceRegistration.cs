using MvcTurbine.ComponentModel;
using Boris.BeekProject.Services;

namespace Boris.BeekProject.Guis.Shared.Ioc
{
    public class SearchServiceRegistration: IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<ISearchService, SearchService>();
        }
    }
}
