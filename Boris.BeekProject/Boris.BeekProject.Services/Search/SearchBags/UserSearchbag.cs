namespace Boris.BeekProject.Services.Search.SearchBags
{
    public class UserSearchbag
    {
        public int? UserId { get; set; }

        public string UserNameStartsWith { get; set; }
        public string UserNameEndsWith { get; set; }
        public string UserNameContains { get; set; }

        //public string FirstNameStartsWith { get; set; }
        //public string FirstNameEndsWith { get; set; }
        //public string FirstNameContains { get; set; }

        //public string LastNameStartsWith { get; set; }
        //public string LasttNameEndsWith { get; set; }
        //public string LasttNameContains { get; set; }
    }
}
