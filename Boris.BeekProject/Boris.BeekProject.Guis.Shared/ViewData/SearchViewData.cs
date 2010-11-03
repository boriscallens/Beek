using System.Collections.Generic;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services.Search.SearchBags;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class SearchViewData: BaseBeekViewData
    {
        public BeekSearchbag UsedBeekSearchBag { get; set; }
        public IList<BaseBeek> FoundBeek { get; set; }

        public SearchViewData()
        {
            FoundBeek = new List<BaseBeek>();
            UsedBeekSearchBag = new BeekSearchbag();
        }
    }
}
