using System.Web.Mvc;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.Utils.Mvc.Attributes
{
    public class AuthenticateAttribute : AuthorizeAttribute
    {
        private IUserRepository userRepository;

        public AuthenticateAttribute()
        {
            
        }
        public AuthenticateAttribute(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}