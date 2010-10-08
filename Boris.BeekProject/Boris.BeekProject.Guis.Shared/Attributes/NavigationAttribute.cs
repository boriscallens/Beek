using System.Web.Mvc;

namespace Boris.BeekProject.Guis.Shared.Attributes
{
    public class NavigationAttribute: ActionFilterAttribute
    {
        private readonly NavigationBlocks navigationBlock;

        public NavigationAttribute(NavigationBlocks currentNavigationBlock)
        {
            navigationBlock = currentNavigationBlock;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            if (controller != null && controller.ViewData is INavigatableViewData)
            {
                ((INavigatableViewData) controller.ViewData).NavBlock = navigationBlock;
            }
        }
    }

    public interface INavigatableViewData
    {
        NavigationBlocks NavBlock { get; set; }
    }

    public enum NavigationBlocks
    {
        Home, Search, MyStuff,
        Beek
    }
}