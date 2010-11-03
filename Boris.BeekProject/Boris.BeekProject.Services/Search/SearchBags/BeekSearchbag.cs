using System.Linq;

namespace Boris.BeekProject.Services.Search.SearchBags
{
    public class BeekSearchbag
    {
        public int? BeekId { get; set; }

        public string BeekTitleStartsWith { get; set; }
        public string BeekTitleEndsWith { get; set; }
        public string BeekTitleContains { get; set; }

        public string AuthorNameStartsWith { get; set; }
        public string AuthorNameEndsWith { get; set; }
        public string AuthorNameContains { get; set; }

        public int? PublishDate { get; set; }
        public int? PublishDateBefore { get; set; }
        public int? PublishDateAfter { get; set; }

        public string IsbnStartsWith { get; set; }
        public string IsbnEndsWith { get; set; }
        public string IsbnContains { get; set; }

        public bool HasValues()
        {
            return GetType().GetProperties().Any(p => p.GetValue(this, null) != null);
        }
    }
}
