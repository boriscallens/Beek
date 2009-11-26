using System;
using System.Web;
namespace BeekProject.Web.Helpers
{
    public static class PathHelper
    {
        public static string ContentRoot {
            get { return VirtualPathUtility.ToAbsolute("~/Content"); }
        }
        public static string PartialsRoot {
            get { return VirtualPathUtility.ToAbsolute("~/Views/Shared/partials"); }
        }
        public static string XmlRoot
        {
            get { return string.Format("{0}/{1}", ContentRoot, "xml"); }
        }
        public static string CssRoot {
            get { return string.Format("{0}/{1}", ContentRoot, "css"); }
        }
        public static string PicRoot
        {
            get { return string.Format("{0}/{1}", ContentRoot, "pics"); }
        }
        public static string JsRoot
        {
            get { return string.Format("{0}/{1}", ContentRoot, "js"); }
        }
        public static string CssUrl(string cssFileName) { 
            return String.Format("{0}/{1}",CssRoot, cssFileName);
        }
        public static string JsUrl(string jsFileName)
        {
            return String.Format("{0}/{1}", JsRoot, jsFileName);
        }
        public static string PicsUrl(string picFileName)
        {
            return String.Format("{0}/{1}", PicRoot, picFileName);
        }
        public static string XmlUrl(string xmlFileName) {
            return String.Format("{0}/{1}", XmlRoot, xmlFileName);
        }
        public static string PartialsUrl(string partialFileName) {
            return string.Format("{0}/{1}", PartialsRoot, partialFileName);
        }
        //public static string XmlFilePath(string xmlFileName)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
