using System.Collections.Generic;
using Boris.BeekProject.Guis.Shared.ViewModels;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class AccountViewData: BaseBeekViewData
    {
        public ViewUser ViewUser;
        public IEnumerable<BaseBeek> Beek;
    }
}
