using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boris.BeekProject.Model.Beek;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Shared.ViewModels{

    public class BaseBeekModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("ISBN")]
        [RegularExpression(@"^ISBN\s(?=[-0-9xX ]{13}$)(?:[0-9]+[- ]){3}[0-9]*[xX0-9]$")]
        public string Isbn { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public BeekTypes Type { get; set; }

        [UIHint("IsFiction")]
        [DisplayName("")]
        public bool IsFiction { get; set; }

        [DisplayName("Other parts of this series")]
        public BeekCollection Collection { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Volume number")]
        public int VolumeNumber { get; set; }

        [ScaffoldColumn(false)]
        public char? SubVolume { get; set; }

        [ScaffoldColumn(false)]
        public int TotalVolumes {get; set;}

        [ScaffoldColumn(false)]
        public IEnumerable<KeyValuePair<IUser, Roles>> Involvements { get; set;}

        [ScaffoldColumn(false)]
        public IEnumerable<BaseGenre> Genres { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<WritingStyle> WritingStyles { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<KeyValuePair<BaseBeek, BeekRelationTypes>> Relations { get; set; }
    }

    public enum BeekTypes
    {
        [Description("Short Story")]
        ShortStory,
        [Description("Long Story")]
        LongStory,
        [Description("Comic")]
        Comic,
        [Description("Poem")]
        Poem,
        [Description("Omnibus")]
        Omnibus
    }
    public enum BeekRelationTypes
    {
        [Description("Original")]
        Original,
        [Description("Republishment")]
        Republishment,
        [Description("Translation")]
        Translation,
        [Description("Adaptation")]
        Adaptation,
        [Description("Update")]
        Update,
        [Description("Complement")]
        Complement
    }
}
