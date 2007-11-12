using System;
using System.Collections.Generic;
using Beek.Helpers;

namespace Beek.DataLayer
{   
    class Print
    {
        private string isbn = string.Empty;
        private Beek original;
        private DateTime publishDate;
        private List<Writer> writers;
        private List<Publisher> publishers;
        private int pages = 0;
        private Language language = Language.Unspecified;
        private List<Illustrator> illustrators;
        private int targetAgePublisher;
        private List<string> quotes;
        private Category category = Category.Unspecified;
        private List<Format> formats;

        public Print(Beek beek)
        {
            writers = new List<Writer>();
            publishers = new List<Publisher>();
            illustrators = new List<Illustrator>();
            quotes = new List<string>();
            formats = new List<Format>();
            formats.Add(Format.Unspecified);
        }
    }
}
