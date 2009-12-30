using Boris.BeekProject.Model.DataAccess.Db4o;
using MvcTurbine.ComponentModel;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.Ioc
{
    public class BeekRepositoryRegistration: IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IBeekRepository, BeekRepository>();
        }
    }
}
