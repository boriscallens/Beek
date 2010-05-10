using System.Configuration;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Accounts;
using Boris.Utils.IO;
using Db4objects.Db4o;
using Microsoft.Practices.Unity;
using User = Boris.BeekProject.Model.Accounts.User;

namespace Boris.BeekProject.Guis.Shared
{
    public static class MvcTurbineContainerFactory
    {
        private static IUnityContainer container;

        public static IUnityContainer CreateUnityContainer()
        {
            if (container != null)
            {
                return container;
            }

            container = new UnityContainer();
            container.RegisterInstance(
                Db4oFactory.OpenServer(
                    IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["beekRepository.path.db4o"]), 0));
            container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());
            container.Configure<InjectedMembers>()
                .ConfigureInjectionFor<UserRepository>(
                    new InjectionConstructor()
                );
            container.RegisterType<IUser, User>();

            container.RegisterType<IBeekRepository, BeekRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISearchService, SearchService>(new ContainerControlledLifetimeManager());
            return container;
        }
    }
}
