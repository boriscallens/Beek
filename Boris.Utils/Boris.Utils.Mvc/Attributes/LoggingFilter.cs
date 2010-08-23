using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Boris.Utils.Logging;

namespace Boris.Utils.Mvc.Attributes
{
    public class LoggingFilter : ActionFilterAttribute
    {
        public ILoggingService LoggingService { get; set; }
        private const string className = "LoggerAttribute";

        public LoggingFilter()
        {
            LoggingService = new NullLoggingService();
        }

        //[System.Diagnostics.DebuggerHidden]
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LoggingService.Log(className, LogLevels.Info,
                String.Format("=> {0,-21} | {1, -15} | {2} | {3} v.{4} js {5}",
                                      DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss K"),
                                      filterContext.HttpContext.Request.UserHostAddress,
                                      filterContext.HttpContext.Request.UrlReferrer == null
                                          ? string.Empty
                                          : filterContext.HttpContext.Request.UrlReferrer.OriginalString,
                                      filterContext.HttpContext.Request.Browser.Browser,
                                      filterContext.HttpContext.Request.Browser.Version,
                                      filterContext.HttpContext.Request.Browser.JScriptVersion)
            );
            if (filterContext.Exception == null)
            {
                LoggingService.Log(className, LogLevels.Info,
                    String.Format("{0,-27}   {1}.{2}({3}) => {4}",
                        ' ',
                        filterContext.Controller.GetType().Name,
                        filterContext.RouteData.Values["action"],
                        GetActionValuesString(filterContext.RouteData.Values),
                        filterContext.Result.GetType().Name
                    )
                );
            }
            else
            {
                LoggingService.Log(className,
                    String.Format("{0,-27}   {1}.{2}({3}) => {4}",
                                    "EXCEPTION",
                                    filterContext.Controller.GetType().Name,
                                    filterContext.RouteData.Values["action"],
                                    GetActionValuesString(filterContext.RouteData.Values),
                                    filterContext.Exception.Message),
                    filterContext.Exception
                );
            }
        }
        //[System.Diagnostics.DebuggerHidden]
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LoggingService.Log(className, LogLevels.Info,
                String.Format("<= {0,-21} | {1, -15} | {2} | {3} v.{4} js {5}",
                                      DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss K"),
                                      filterContext.HttpContext.Request.UserHostAddress,
                                      filterContext.HttpContext.Request.UrlReferrer == null
                                          ? string.Empty
                                          : filterContext.HttpContext.Request.UrlReferrer.OriginalString,
                                      filterContext.HttpContext.Request.Browser.Browser,
                                      filterContext.HttpContext.Request.Browser.Version,
                                      filterContext.HttpContext.Request.Browser.JScriptVersion)
            );
            LoggingService.Log(className, LogLevels.Info,
                String.Format("{0,-27}   {1}.{2}({3})",
                        ' ',
                        filterContext.Controller.GetType().Name,
                        filterContext.RouteData.Values["action"],
                        GetActionValuesString(filterContext.RouteData.Values))
            );
        }

        private static string GetActionValuesString(RouteValueDictionary dictionary)
        {
            if (dictionary == null || !dictionary.Keys.Any())
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, object> valuePair in dictionary)
            {
                sb.AppendFormat("{0} = \"{1}\"; ",
                                valuePair.Key,
                                valuePair.Value);
            }
            return sb.ToString();
        }
    }
}