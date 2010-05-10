using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services.Search;
using Boris.BeekProject.Services.Search.SearchBags;

namespace Boris.BeekProject.Services
{
    public interface ISearchService
    {
        IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag, int skip, int take);
        IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag);
        IEnumerable<IUser> SearchUsers(UserSearchbag bag, int skip, int take);
        IEnumerable<IUser> SearchUsers(UserSearchbag bag);
        IEnumerable<IUser> SearchUsers(IEnumerable<UserSearchbag> bags);
        IEnumerable<IUser> SearchUsers(IEnumerable<UserSearchbag> bags, int skip, int take);
    }
}
