using Boris.BeekProject.Guis.Shared;
using Boris.BeekProject.Misc.BeekFiller.Controller;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services;
using Microsoft.Practices.Unity;

namespace Boris.BeekProject.Misc.BeekFiller
{
    class Program
    {
        private static IUnityContainer container;

        static void Main(string[] args)
        {
            container = MvcTurbineContainerFactory.CreateUnityContainer();
            IsbnDbProxy isbnDbProxy = container.Resolve<IsbnDbProxy>();

            string title = args[0];
            BaseBeek beek = isbnDbProxy.SearchByTitle(title);
        }
    }
}
