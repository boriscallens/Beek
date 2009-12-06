using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;

namespace Boris.BeekProject.Guis.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}