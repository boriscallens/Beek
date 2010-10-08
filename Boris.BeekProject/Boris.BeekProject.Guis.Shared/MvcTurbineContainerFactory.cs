using System.Configuration;
using System.Web;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Model.DataAccess.Db4o;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Services.Search;
using Boris.Utils.IO;
using Boris.Utils.Logging;
using Boris.Utils.Logging.NLog;
using Db4objects.Db4o;
using Ninject;
using User = Boris.BeekProject.Model.Accounts.User;

namespace Boris.BeekProject.Guis.Shared
{
    public static class MvcTurbineContainerFactory
    {
        private static IKernel kernel;

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
                .To<HttpAccountService>()
                .InRequestScope()
                .WithConstructorArgument("context",x => GetCurrentContext());
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
            kernel.Bind<ILoggingService>()
                .To<NlogLoggingService>()
                .InSingletonScope();
            return kernel;
        }
        private static HttpContext GetCurrentContext()
        {
            return HttpContext.Current;
        }

    }
}
