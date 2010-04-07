using System;
using System.Collections.Generic;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.DataAccess;
using Boris.BeekProject.Services.Search;
using Boris.Utils.Strings;

namespace Boris.BeekProject.Services
{
    public class SearchService: ISearchService
    {
        private readonly IBeekRepository beekRepository;

        public SearchService(IBeekRepository beekRepository)
        {
            this.beekRepository = beekRepository;
        }

        public IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag, int skip, int take)
        {
            return SearchBeek(bag).Skip(skip).Take(take);
        }
        public IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag)
        {
            var q = beekRepository.GetBeek();
            if(bag.BeekId.HasValue)
            {
                q = q.Where(b => b.Id.Equals(bag.BeekId));
            }
            if (!string.IsNullOrEmpty(bag.BeekTitleStartsWith))
            {
                q = q.Where(b => b.Title.StartsWith(bag.BeekTitleStartsWith, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(bag.BeekTitleEndsWith))
            {
                q = q.Where(b => b.Title.EndsWith(bag.BeekTitleStartsWith, StringComparison.OrdinalIgnoreCase));
            }
            if(!string.IsNullOrEmpty(bag.BeekTitleContains))
            {
                q = q.Where(b => b.Title.Contains(bag.BeekTitleContains, StringComparison.OrdinalIgnoreCase));
            }
            return q.AsEnumerable();
        }
    }
}