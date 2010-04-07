using System.Web.Mvc;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Shared.ViewModels
{
    public class BaseBeekViewModel : ViewDataDictionary
    {
        public IUser User { get; set; }
        public Dictionary<MessageKeys, string> Messages { get; set; }
        public NavBlocks CurrentNavBlock { get; set; }
    }

    public enum MessageKeys
    {
        UserNameNotFound, FirstTimeVisitor, IsAnonymous
    }
    public enum NavBlocks
    {
        Home, Beek, Search, MyStuff
    }
}
