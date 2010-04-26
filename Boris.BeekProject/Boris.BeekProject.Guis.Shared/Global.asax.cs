using System.Web.Mvc;
using Boris.BeekProject.Guis.Shared.ModelBinding;
using Boris.BeekProject.Guis.Shared.ViewModels.DTO;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using Microsoft.Practices.Unity;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;
using AutoMapper;


namespace Boris.BeekProject.Guis.Shared
{
    public class MvcApplication : TurbineApplication
    {
        private static readonly IUnityContainer container;
        private static readonly UnityServiceLocator provider;

        static MvcApplication()
        {
            container = MvcTurbineContainerFactory.CreateUnityContainer();
            provider = new UnityServiceLocator(container);
            
            ServiceLocatorManager.SetLocatorProvider(() => provider);
            ModelBinders.Binders.Add(typeof(IUser), new UserModelBinder(ServiceLocatorManager.Current.Resolve<IUserRepository>()));
            ModelBinders.Binders.Add(typeof(BaseBeek), new BaseBeekModelBinder(ServiceLocatorManager.Current.Resolve<IBeekRepository>()));

            CreateDTOMappings();
        }

        private static void CreateDTOMappings()
        {
            Mapper.CreateMap<BaseBeek, BaseBeekDTO>();
        }
    }
}