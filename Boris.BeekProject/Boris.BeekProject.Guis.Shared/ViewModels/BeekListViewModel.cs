using System.Collections.Generic;
using Boris.BeekProject.Guis.Shared.ViewModels.DTO;

namespace Boris.BeekProject.Guis.Shared.ViewModels
{
    public class BeekListViewModel: BaseBeekViewModel
    {
        public IEnumerable<BaseBeekDTO> Beeks { get; set; }
    }
}
