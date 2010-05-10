using System.Collections.Generic;
using System.Xml.Linq;
using System.Diagnostics.Contracts;

namespace Boris.BeekProject.Misc.BeekFiller.Controller
{
    class IsbnDbBeek
    {
        private readonly XElement isbnDbBookData;

        public IsbnDbBeek(XElement beekIsbnDbBookData)
        {
            Contract.Requires(beekIsbnDbBookData != null);
            isbnDbBookData = beekIsbnDbBookData;
        }

        public string Title
        {
            get
            {
                string title = isbnDbBookData.Element("TitleLong").Value;
                title = string.IsNullOrWhiteSpace(title) ? isbnDbBookData.Element("Title").Value : title;
                return title;
            }
        }

        public string Isbn
        {
            get { return isbnDbBookData.Attribute("isbn").Value; }
        }

        public IEnumerable<string> Authors
        {
            get { return isbnDbBookData.Element("AuthorsText").Value.Split(','); }
        }
    }
}
