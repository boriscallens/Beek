using System.Collections.Generic;
using Beek.Helpers;

namespace Beek.DataLayer
{
    public class Beek
    {
        private string isbn = string.Empty;
        private string title = string.Empty;
        private List<Writer> writers;
        private bool fiction;
        private List<Genre> genres;

        public Beek()
        {
            genres = new List<Genre>();
            genres.Add(Genre.Unspecified);
        }
    }
}
