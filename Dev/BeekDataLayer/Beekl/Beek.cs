using System.Collections.Generic;
using Beek.Helpers;

namespace Beek.DataLayer
{
    public class Beek
    {
        private string isbn = string.Empty;
        private string title = string.Empty;
        private List<Writer> writers;
        private Fiction fiction = Fiction.Unspecified;
        private List<Genre> genres;
        private WritingStyle writingStyle = WritingStyle.Unspecified;
        private int pages = 0;
        private List<Publisher> publishers;

        public Beek()
        {
            genres = new List<Genre>();
            genres.Add(Genre.Unspecified);
            writers = new List<Writer>();
        }



    }
}
