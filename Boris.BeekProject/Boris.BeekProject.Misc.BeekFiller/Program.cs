using System;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using Ninject;
using Boris.BeekProject.Services;
using Boris.BeekProject.Guis.Shared;
using Boris.BeekProject.Services.Search;

namespace Boris.BeekProject.Misc.BeekFiller
{
    static class Program
    {
        //private static IUnityContainer container;
        private static IKernel kernel;

        static void Main(string[] args)
        {
            kernel = MvcTurbineContainerFactory.CreateNinjectKernel();
            ISearchService remote = kernel.Get<ISearchService>("isbnDbSearchService");
            ISearchService local = kernel.Get<ISearchService>("db4oSearchService");

            var beeks = local.SearchBeek(new BeekSearchbag { BeekTitleContains = args.First() });
            
            if (!beeks.Any())
            {
                beeks = remote.SearchBeek(new BeekSearchbag { BeekTitleContains = args.First() });
                if (beeks.Any())
                {
                    var localBeekRepository = kernel.Get<IBeekRepository>();
                    foreach (BaseBeek beek in beeks)
                    {
                        localBeekRepository.AddBeek(beek);
                    }
                }
            }
            Console.Write("There were ");
            if (beeks.Any())
            {
                Console.Write(beeks.Count());
            }
            else
            {
                Console.Write("no");
            }
            Console.Write(" beeks");
        }
    }
}
