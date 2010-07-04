using System.Collections.Generic;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class BeekViewData: BaseBeekViewData
    {
        public BaseBeekModel Beek { get; set; }
        public IEnumerable<BaseBeekModel> Beeks { get; set;}
    }
}
