using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace Boris.BeekProject.Model.Beek
{
    public class IsbnDbBeek
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


