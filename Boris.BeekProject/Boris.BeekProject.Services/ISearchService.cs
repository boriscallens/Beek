using System.Collections.Generic;
using Boris.BeekProject.Guis.Shared.SearchBags;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Services
{
    public interface ISearchService
    {
        IEnumerable<IBeek> SearchBeek(BeekSearchbag bag, int skip, int take);
        IEnumerable<IBeek> SearchBeek(BeekSearchbag bag);
    }
}
