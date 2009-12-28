using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Shared.ViewModels
{
    public class BeekViewModel : ViewDataDictionary
    {
        public IUser User { get; set; }
        public ISetting Setting { get; set; }
    }
}
