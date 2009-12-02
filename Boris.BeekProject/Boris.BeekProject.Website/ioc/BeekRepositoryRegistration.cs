using MvcTurbine.ComponentModel;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.Db4o;

namespace Boris.BeekProject.Website.ioc
{
    public class BeekRepositoryRegistration: IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<ICatalogRepository, CatalogRepository>();
        }
    }
}
