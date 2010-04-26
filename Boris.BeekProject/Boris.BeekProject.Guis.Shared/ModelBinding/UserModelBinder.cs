using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.ModelBinding
{
    public class UserModelBinder: IModelBinder
    {
        private readonly IUserRepository repository;

        public UserModelBinder(IUserRepository repository)
        {
            this.repository = repository;
        }
       
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            IUser user = repository.GetUser(controllerContext.HttpContext.Request["User.Id"]) ?? repository.CreateAnonymousUser();
            user.Name = controllerContext.HttpContext.Request["User.Name"] ?? user.Name;
            user.Email = controllerContext.HttpContext.Request["User.Email"] ?? user.Email;

            string password = controllerContext.HttpContext.Request["password"];
            if(!string.IsNullOrEmpty(password) && user is User)
            {
                (user as User).SetPassword(password);
            }
            return user;
        }
    }
}