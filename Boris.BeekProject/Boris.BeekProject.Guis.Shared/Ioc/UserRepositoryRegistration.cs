using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using MvcTurbine.ComponentModel;

namespace Boris.BeekProject.Guis.Shared.Ioc
{
    public class UserRepositoryRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IUserRepository, UserRepository>();
        }
    }
}
