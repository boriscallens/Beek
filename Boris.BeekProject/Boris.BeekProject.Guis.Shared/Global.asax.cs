using System.Linq;
using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using MvcTurbine.ComponentModel;
using MvcTurbine.Ninject;
using MvcTurbine.Web;
using AutoMapper;
using Ninject;


namespace Boris.BeekProject.Guis.Shared
{
    public class MvcApplication : TurbineApplication
    {
        private static readonly IKernel ninjectKernel;
        private static readonly NinjectServiceLocator provider;

        static MvcApplication()
        {
            ninjectKernel = MvcTurbineContainerFactory.CreateNinjectKernel();
            provider = new NinjectServiceLocator(ninjectKernel);

            ServiceLocatorManager.SetLocatorProvider(() => provider);
            

            CreateDTOMappings();
        }
        private static void CreateDTOMappings()
        {
            Mapper.CreateMap<BaseBeek, ViewBeek>();
            Mapper.CreateMap<ViewBeek, BaseBeek>();
            Mapper.CreateMap<BaseBeek, ViewBeek>()
                .ForMember(
                    v => v.Author,
                    b => b.MapFrom(
                        v => v.GetInvolvedUsersForContribution(Contributions.Writer)
                            .FirstOrDefault())
                );
            Mapper.CreateMap<IUser, ViewUser>();
        }
    }
}