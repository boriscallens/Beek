using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Services.Accounts;
using Boris.BeekProject.Services.Search.SearchBags;

namespace Boris.BeekProject.Services.Search
{
    public class IsbnDbSearchService: ISearchService
    {
        private readonly string baseRequestUrl;
        private readonly IAccountService accountService;
        private readonly ISearchService accountSearchService;

        public IsbnDbSearchService(string baseRequestUrl, IAccountService accountService, ISearchService accountSearchService)
        {
            this.baseRequestUrl = baseRequestUrl;
            this.accountSearchService = accountSearchService;
            this.accountService = accountService;
        }

        public IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag)
        {
            return ParseToBeek(XDocument.Load(CreateRequest(bag).GetResponse().GetResponseStream()));
        }

        public IEnumerable<IUser> SearchUsers(UserSearchbag bag, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> SearchUsers(UserSearchbag bag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> SearchUsers(IEnumerable<UserSearchbag> bags)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> SearchUsers(IEnumerable<UserSearchbag> bags, int skip, int take)
        {
            throw new NotImplementedException();
        }

        private WebRequest CreateRequest(BeekSearchbag bag)
        {
            string request;
            if (!string.IsNullOrWhiteSpace(bag.BeekTitleContains))
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
                    var users = accountSearchService.SearchUsers(bags);
                    Parallel.ForEach(users, user => user.AddRole(Roles.Writer));
                    beek.InvolveUsers(users, Roles.Writer);
                }
                yield return beek;
            }
        }
    }
}
