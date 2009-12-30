using System.Collections.Generic;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services.Search;

namespace Boris.BeekProject.Services
{
    public interface ISearchService
    {
        IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag, int skip, int take);
        IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag);
    }
}
