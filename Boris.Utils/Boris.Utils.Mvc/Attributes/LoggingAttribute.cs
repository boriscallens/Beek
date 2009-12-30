using System;
using MvcTurbine.Web.Controllers;

namespace Boris.Utils.Mvc.Attributes
{
    public class CustomActionAttribute : InjectableFilterAttribute
    {
        public override Type FilterType
        {
            get { return typeof(LoggingFilter); }
        }
    }
}
