using System.Web.Mvc;
using System.Configuration;
using AutoMapper;
using MvcTurbine.Web;
using MvcTurbine.Unity;
using Db4objects.Db4o;
using Microsoft.Practices.Unity;
using MvcTurbine.ComponentModel;
using Boris.Utils.IO;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using Boris.BeekProject.Guis.Shared.ModelBinding;
using User = Boris.BeekProject.Model.Accounts.User;
using Boris.BeekProject.Guis.Shared.ViewModels.DTO;


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
            // ViewEngines.Engines.Clear();
            // ViewEngines.Engines.Add(new NHamlMvcViewEngine());

            ServiceLocatorManager.SetLocatorProvider(() => provider);
            ModelBinders.Binders.Add(typeof(IUser), new UserModelBinder(ServiceLocatorManager.Current.Resolve<IUserRepository>()));
            ModelBinders.Binders.Add(typeof(BaseBeek), new BaseBeekModelBinder(ServiceLocatorManager.Current.Resolve<IBeekRepository>()));

            CreateDTOMappings();
        }

        private static void CreateDTOMappings()
        {
            Mapper.CreateMap<BaseBeek, BaseBeekDTO>();
        }

        private static IUnityContainer CreateContainer()
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
            container.Configure<InjectedMembers>()
                .ConfigureInjectionFor<UserRepository>(
                    new InjectionConstructor()
                );
            container.RegisterType<IUser, User>();

            container.RegisterType<IBeekRepository, BeekRepository>(new ContainerControlledLifetimeManager());
            
            return container;
        }
        
    }
}