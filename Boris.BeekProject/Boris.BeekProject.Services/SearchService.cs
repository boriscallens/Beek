using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Boris.BeekProject.Model.Beek;
using Boris.Utils.Strings;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace Boris.BeekProject.Services
{
    public static class SearchService
    {
        private static readonly IObjectServer server =
            Db4oFactory.OpenServer(ConfigurationManager.AppSettings["db4o.beekRepository.path"], 0);
        private static readonly IObjectContainer client = server.OpenClient();

        public static IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag, int skip, int take)
        {
            return SearchBeek(bag).Skip(skip).Take(take);
        }

        public static IEnumerable<BaseBeek> SearchBeek(BeekSearchbag bag)
        {
            var q = client.Cast<BaseBeek>();
            if(bag.BeekId != default(Guid))
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

    public class BeekSearchbag
    {
        public Guid BeekId { get; set; }
        public String BeekTitleStartsWith { get; set; }
        public String BeekTitleEndsWith { get; set; }
        public string BeekTitleContains { get; set; }
    }
}
