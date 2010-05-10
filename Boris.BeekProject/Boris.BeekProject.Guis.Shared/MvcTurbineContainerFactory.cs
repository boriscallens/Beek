using System.Configuration;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Services.Search;
using Boris.Utils.IO;
using Db4objects.Db4o;
using Microsoft.Practices.Unity;
using Ninject;
using User = Boris.BeekProject.Model.Accounts.User;

namespace Boris.BeekProject.Guis.Shared
{
    public static class MvcTurbineContainerFactory
    {
        private static IUnityContainer container;
        private static IKernel kernel;

        public static IUnityContainer CreateUnityContainer()
        {
            if (container != null)
            {
                return container;
            }

            container = new UnityContainer();
            container.RegisterInstance("db4oBeekServer", Db4oFactory.OpenServer(IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["beekRepository.path.db4o"]), 0));

            container.RegisterType<IBeekRepository, Db4oBeekRepository>(new ContainerControlledLifetimeManager());
            
            container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUser, User>();



            container.Configure<InjectedMembers>()
                .ConfigureInjectionFor<UserRepository>(
                    new InjectionConstructor(new{})
                );
            
            container.RegisterType<ISearchService, SearchService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISearchService, SearchService>("IsbnDbSearch", new ContainerControlledLifetimeManager());
            //container.Configure<InjectedMembers>()
            //    .ConfigureInjectionFor<SearchService>("IsbnDbSearch",
            //    new InjectionMember[]{new InjectionMember{container.Resolve<IsbnDbBeekRepository>("IsbnDbBeeks")}});
            //container.Configure<InjectedMembers>()
            //    .ConfigureInjectionFor<UserRepository>(
            //        new InjectionConstructor(new{})
            //    );

            container.RegisterInstance(ConfigurationManager.AppSettings["isbnDb.baseRequestString"]);

            return container;
        }

        public static IKernel CreateNinjectKernel()
        {
            if (kernel != null)
            {
                return kernel;
            }
            kernel = new StandardKernel();
            kernel.Bind<IBeekRepository>()
                .To<Db4oBeekRepository>()
                .InSingletonScope()
                .WithConstructorArgument("beekServer", Db4oFactory.OpenServer(IOHelper.MakeAbsolute(ConfigurationManager.AppSettings["beekRepository.path.db4o"]), 0));
            kernel.Bind<IUserRepository>()
                .To<UserRepository>()
                .InSingletonScope();
            kernel.Bind<IAccountService>()
                .To<AccountService>()
                .InSingletonScope();
            kernel.Bind<IUser>()
                .To<User>();
            kernel.Bind<ISearchService>()
                .To<SearchService>()
                .InSingletonScope()
                .Named("db4oSearchService");
            kernel.Bind<ISearchService>()
                .To<IsbnDbSearchService>()
                .InSingletonScope()
                .Named("isbnDbSearchService")
                .WithConstructorArgument("baseRequestUrl", ConfigurationManager.AppSettings["isbnDb.baseRequestString"]);
            return kernel;
        }
    }
}
