using System.Collections.Generic;
using Boris.BeekProject.Model.Beek;
namespace Boris.BeekProject.Guis.Shared.ViewModels
{
    public class SearchViewModel: BaseBeekViewModel
    {
        public IEnumerable<BaseBeek> FoundBeek { get; set; }

        public SearchViewModel()
        {
            FoundBeek = new List<BaseBeek>();
        }
    }
}
