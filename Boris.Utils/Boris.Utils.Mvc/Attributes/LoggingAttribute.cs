using System;
using MvcTurbine.Web.Controllers;

namespace Boris.Utils.Mvc.Attributes
{
    public class LoggingAttribute : InjectableFilterAttribute
    {
        public override Type FilterType
        {
            get { return typeof(LoggingFilter); }
        }
    }
}
