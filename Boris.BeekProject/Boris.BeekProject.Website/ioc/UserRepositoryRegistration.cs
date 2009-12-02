using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.Db4o;
using MvcTurbine.ComponentModel;

namespace Boris.BeekProject.Website.ioc
{
    public class UserRepositoryRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IUserRepository, UserRepository>();
        }
    }
}
