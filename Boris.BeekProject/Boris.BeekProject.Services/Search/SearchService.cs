﻿using System;
using System.Collections.Generic;
using System.Linq;
using Boris.Utils.Strings;
using Boris.BeekProject.Model.Accounts;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Services.Search.SearchBags;

namespace Boris.BeekProject.Services.Search
{
    public class SearchService: ISearchService
    {
        private readonly IBeekRepository beekRepository;
        private readonly IUserRepository userRepository;

        public SearchService(IBeekRepository beekRepository, IUserRepository userRepository)
        {
            this.beekRepository = beekRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag, int skip, int take)
        {
            return SearchBeek(bag).Skip(skip).Take(take);
        }
        public IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag)
        {
            if(!bag.HasValues())
            {
                return new List<BaseBeek>();
            }

            var q = beekRepository.GetBeek();
            if(bag.BeekId.HasValue)
            {
                q = q.Where(b => b.Id.Equals(bag.BeekId));
            }

            if (!string.IsNullOrEmpty(bag.BeekTitleStartsWith))
            {
                q = q.Where(b => b.Title.StartsWith(bag.BeekTitleStartsWith, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(bag.BeekTitleEndsWith))
            {
                q = q.Where(b => b.Title.EndsWith(bag.BeekTitleEndsWith, StringComparison.InvariantCultureIgnoreCase));
            }
            if(!string.IsNullOrEmpty(bag.BeekTitleContains))
            {
                q = q.Where(b => b.Title.Contains(bag.BeekTitleContains, StringComparison.InvariantCultureIgnoreCase));
            }


            if(!string.IsNullOrEmpty(bag.IsbnContains))
            {
                q = q.Where(b => b.Isbn.Contains(bag.IsbnContains, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(bag.IsbnStartsWith))
            {
                q = q.Where(b => b.Isbn.StartsWith(bag.IsbnContains, StringComparison.InvariantCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(bag.IsbnEndsWith))
            {
                q = q.Where(b => b.Isbn.EndsWith(bag.IsbnContains, StringComparison.InvariantCultureIgnoreCase));
            }

            if(!string.IsNullOrEmpty(bag.AuthorNameContains))
            {
                q = q.Where(b => b.GetInvolvedUsersForContribution(Contributions.Writer).Any(u=>u.Name.Contains(bag.AuthorNameContains, StringComparison.InvariantCultureIgnoreCase)));
            }
            if (!string.IsNullOrEmpty(bag.AuthorNameEndsWith))
            {
                q = q.Where(b => b.GetInvolvedUsersForContribution(Contributions.Writer).Any(u => u.Name.EndsWith(bag.AuthorNameEndsWith, StringComparison.InvariantCultureIgnoreCase)));
            }
            if (!string.IsNullOrEmpty(bag.AuthorNameStartsWith))
            {
                q = q.Where(b => b.GetInvolvedUsersForContribution(Contributions.Writer).Any(u => u.Name.StartsWith(bag.AuthorNameStartsWith, StringComparison.InvariantCultureIgnoreCase)));
            }

            return q.AsEnumerable();
        }

        public IEnumerable<IUser> SearchUsers(UserSearchbag bag, int skip, int take)
        {
            return SearchUsers(bag).Skip(skip).Take(take);
        }
        public IEnumerable<IUser> SearchUsers(UserSearchbag bag)
        {
            var q = userRepository.GetUsers();
            if(bag.UserId.HasValue)
            {
                q = q.Where(u => u.Id.Equals(bag.UserId));
            }
            if (!string.IsNullOrEmpty(bag.UserNameStartsWith))
            {
                q = q.Where(b => b.Name.StartsWith(bag.UserNameStartsWith, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(bag.UserNameEndsWith))
            {
                q = q.Where(b => b.Name.EndsWith(bag.UserNameEndsWith, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(bag.UserNameContains))
            {
                q = q.Where(b => b.Name.Contains(bag.UserNameContains, StringComparison.OrdinalIgnoreCase));
            }
            return q.AsEnumerable();
        }
        public IEnumerable<IUser> SearchUsers(IEnumerable<UserSearchbag> bags, int skip, int take)
        {
            return SearchUsers(bags).Skip(skip).Take(take);
        }
        public IEnumerable<IUser> SearchUsers(IEnumerable<UserSearchbag> bags)
        {
            var q = userRepository.GetUsers();
            foreach (UserSearchbag bag in bags)
            {
                var subQuery = userRepository.GetUsers();
                UserSearchbag bag1 = bag;
                if (bag.UserId.HasValue)
                {
                    subQuery = subQuery.Where(u => u.Id.Equals(bag1.UserId));
                }
                if (!string.IsNullOrEmpty(bag.UserNameStartsWith))
                {
                    subQuery = subQuery.Where(b => b.Name.StartsWith(bag1.UserNameStartsWith, StringComparison.OrdinalIgnoreCase));
                }
                if (!string.IsNullOrEmpty(bag.UserNameEndsWith))
                {
                    subQuery = subQuery.Where(b => b.Name.EndsWith(bag1.UserNameEndsWith, StringComparison.OrdinalIgnoreCase));
                }
                if (!string.IsNullOrEmpty(bag.UserNameContains))
                {
                    subQuery = subQuery.Where(b => b.Name.Contains(bag1.UserNameContains, StringComparison.OrdinalIgnoreCase));
                }
                q = q.Intersect(subQuery);
            }
            return q.AsEnumerable();
        }
    }
}