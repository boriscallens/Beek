using System.Globalization;
namespace Boris.BeekProject.Model.Beek
{
    public class Translation : IBeekRelationType
    {
        public string Label
        {
            get { return "Translation"; }
        }
        public string Description
        {
            get { return "{0} is the translation of {1} in "+Culture.EnglishName; }
        }
        public CultureInfo Culture { get; private set; }

        public Translation(CultureInfo culture)
        {
            this.Culture = culture;   
        }
        public bool Equals(IBeekRelationType other)
        {
            return other is Translation && ((Translation)other).Culture.Equals(Culture);
        }
    }
}