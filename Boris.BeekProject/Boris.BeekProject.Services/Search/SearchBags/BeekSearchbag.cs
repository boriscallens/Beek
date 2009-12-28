namespace Boris.BeekProject.Services.Search
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
    }
}
