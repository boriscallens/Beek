namespace Boris.BeekProject.Model.Beek
{
    public class Original: IBeekRelationType
    {
        public bool Equals(IBeekRelationType other)
        {
            return other is Original;
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