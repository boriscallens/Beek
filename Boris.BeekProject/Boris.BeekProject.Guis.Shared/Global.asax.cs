using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using Microsoft.Practices.Unity;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;
using System.Web.Mvc;
using NHaml.Web.Mvc;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Shared
{
    public class MvcApplication : TurbineApplication
    {
        private static IUnityContainer container;
        private static readonly UnityServiceLocator provider;

        static MvcApplication()
        {
            container = CreateContainer();
            provider = new UnityServiceLocator(CreateContainer());
            
            // ToDo: add this to view registerations once we can use MvcTurbine bits
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new NHamlMvcViewEngine());

            ServiceLocatorManager.SetLocatorProvider(() => provider);
        }

        private static IUnityContainer CreateContainer()
        {
            if (container != null)
            {
                return container;
            }

            container = new UnityContainer();

            container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            container.Configure<InjectedMembers>()
                .ConfigureInjectionFor<UserRepository>(
                    new InjectionConstructor()
                );
            container.RegisterType<IUser, User>();
            return container;
        }
    }
}