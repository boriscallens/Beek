using System.Web.Mvc;

namespace BeekProject.Web.Helpers
{
    public static class MvcHelper
    {
        public static string RenderPartial(this HtmlHelper helper, string partialName, object controlData){
            return helper.RenderUserControl(PathHelper.PartialsUrl(partialName), controlData) ;
        }
        public static string RenderPartial(this HtmlHelper helper, string partialName)
        {
            return helper.RenderUserControl(PathHelper.PartialsUrl(partialName));
        }
    }
}
