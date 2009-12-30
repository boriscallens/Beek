namespace Boris.BeekProject.Model.Beek
{
    public class Republishing : IBeekRelationType
    {
        public bool Equals(IBeekRelationType other)
        {
            // ToDo: what makes one republishing different from the other?
            return false;
        }

        public string Label
        {
            get { return "Original"; }
        }
        public string Description
        {
            get { return "{0} is the original of {1}"; }
        }
    }
}