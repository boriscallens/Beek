using System.Collections.Generic;

namespace Boris.BeekProject.Model.Beek
{
    public class Beek
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public bool IsFiction { get; set; }
        public IList<Genre> Genres { get; set; }
        public WritingStyle WritingStyle { get; set; }
        public BluePrint BluePrint { get; set; }
    }
}
