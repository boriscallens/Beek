using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Services;
using Boris.BeekProject.Services.Search;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Services.Search.SearchBags;

namespace Boris.BeekProject.Misc.BeekFiller.Controller
{
    public class IsbnDbProxy
    {
        private const string baseRequestUrl = @"http://isbndb.com/api/books.xml?access_key=DM43UJGL&index1={0}&value1={1}";
        private readonly ISearchService searchService;
        private readonly IAccountService accountService;

        public IsbnDbProxy(ISearchService searchService, IAccountService accountService)
        {
            this.searchService = searchService;
            this.accountService = accountService;
        }

        public BaseBeek SearchByTitle(string title)
        {
            BeekSearchbag bag = new BeekSearchbag { BeekTitleContains = title };
            return searchService.SearchBeek(bag).FirstOrDefault() ??
                   ParseToBeek(XDocument.Load(CreateRequest(bag).GetResponse().GetResponseStream())).FirstOrDefault();
        }

        private static WebRequest CreateRequest(BeekSearchbag bag)
        {
            string request;
            if(!string.IsNullOrWhiteSpace(bag.BeekTitleContains))
            {
                request = string.Format(baseRequestUrl, "title", HttpUtility.UrlEncode(bag.BeekTitleContains));
            }
            else
            {
                throw new NotImplementedException();
            }
            return WebRequest.Create(request);
        }
        private IEnumerable<BaseBeek> ParseToBeek(XDocument doc)
        {
            foreach (XElement bookData in doc.Descendants("BookData"))
            {
                IsbnDbBeek isbnDbBeek = new IsbnDbBeek(bookData);

                BaseBeek beek = new BaseBeek(BeekTypes.LongStory)
                        {
                            Title = isbnDbBeek.Title,
                            Isbn = isbnDbBeek.Isbn
                        };

                if (isbnDbBeek.Authors.Any())
                {
                    //For users that don't exist yet, we'll create them..
                    accountService.CreateUsersInBatch(
                        isbnDbBeek.Authors.Where(name => !accountService.DoesUserExist(name)), Roles.Writer, Sources.IsbnDb);

                    //now add al those users!
                    IEnumerable<UserSearchbag> bags =
                        isbnDbBeek.Authors.Select(authorName => new UserSearchbag { UserNameContains = authorName });
                    var users = searchService.SearchUsers(bags);
                    Parallel.ForEach(users, user => user.AddRole(Roles.Writer));
                    beek.InvolveUsers(users, Roles.Writer);
                }
                yield return beek;
            }
        }
    }
}
