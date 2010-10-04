using System.Collections.Generic;
using Boris.BeekProject.Guis.Shared.ViewModels;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class BeekViewData: BaseBeekViewData
    {
        public ViewBeek Beek { get; set; }
        public IEnumerable<ViewBeek> Beeks { get; set;}
    }
}
