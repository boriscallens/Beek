using System.Collections.Generic;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class SearchViewData: BaseBeekViewData
    {
        public IEnumerable<BaseBeek> FoundBeek { get; set; }

        public SearchViewData()
        {
            FoundBeek = new List<BaseBeek>();
        }
    }
}
