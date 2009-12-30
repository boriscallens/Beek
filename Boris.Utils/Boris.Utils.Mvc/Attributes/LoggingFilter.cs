using System;
using System.Web.Mvc;

namespace Boris.Utils.Mvc.Attributes
{
    public class LoggingFilter: IActionFilter
    {
        private ILoggerService logger;

        public LoggingFilter(ILoggerService loggerService)
        {
            logger = loggerService;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}