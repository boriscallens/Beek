using System;
using System.Globalization;
using System.Web.Mvc;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;

namespace Boris.BeekProject.Guis.Shared.ModelBinding
{
    public class BaseBeekModelBinder: IModelBinder
    {
        private readonly IBeekRepository repository;

        public BaseBeekModelBinder(IBeekRepository repository)
        {
            this.repository = repository;
        }
       
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            BeekTypes type;
            try
            {
                type = (BeekTypes)Enum.Parse(typeof (BeekTypes), controllerContext.HttpContext.Request["Beek.Type"]);
            }
            catch (Exception)
            {
                type = BeekTypes.LongStory;
            }
            BaseBeek newBeek = new BaseBeek(type)
                                   {
                                       Title = controllerContext.HttpContext.Request["Beek.Title"],
                                   };
            if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request["Beek.Isbn"]))
            {
                string isbn =
                    controllerContext.HttpContext.Request["Beek.Isbn"].ToUpper(CultureInfo.InvariantCulture).Replace(
                        "ISBN", "").Trim();
                newBeek.Isbn = isbn;
            }
            bool isFiction;
            if (bool.TryParse(controllerContext.HttpContext.Request["Beek.IsFiction"], out isFiction))
            {
                newBeek.IsFiction = isFiction;
            }
            
            //ToDo: all the other beek fields...
            return newBeek;
        }
    }
}