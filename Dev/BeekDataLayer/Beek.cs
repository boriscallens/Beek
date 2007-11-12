using System.Collections.Generic;
using Beek.Helpers;

namespace Beek.DataLayer
{
    public class Beek
    {
        private string isbn = string.Empty;
        private string title = string.Empty;
        
        private Fiction fiction = Fiction.Unspecified;
        private List<Genre> genres;
        private WritingStyle writingStyle = WritingStyle.Unspecified;
        private List<Beek> restOfSeries;
        private List<Print> prints;

        public Beek()
        {
            genres = new List<Genre>();
            genres.Add(Genre.Unspecified);
            restOfSeries = new List<Beek>();
            prints = new List<Print>();
        }

        
    }
}
