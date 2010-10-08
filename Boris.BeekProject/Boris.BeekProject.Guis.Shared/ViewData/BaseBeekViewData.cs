using System.Web.Mvc;
using System.Collections.Generic;
using Boris.BeekProject.Guis.Shared.Attributes;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class BaseBeekViewData : ViewDataDictionary, INavigatableViewData
    {
        public IUser User { get; set; }
        public readonly Dictionary<MessageKeys, string> Messages = new Dictionary<MessageKeys, string>();
        public NavigationBlocks NavBlock { get; set; }
    }

    public enum MessageKeys
    {
        UserNameNotFound, FirstTimeVisitor, IsAnonymous,
        UserNameInUse
    }

}
