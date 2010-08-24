using System.Web.Mvc;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class BaseBeekViewData : ViewDataDictionary
    {
        public IUser User { get; set; }
        public readonly Dictionary<MessageKeys, string> Messages = new Dictionary<MessageKeys, string>();
    }

    public enum MessageKeys
    {
        UserNameNotFound, FirstTimeVisitor, IsAnonymous,
        UserNameInUse
    }

}
