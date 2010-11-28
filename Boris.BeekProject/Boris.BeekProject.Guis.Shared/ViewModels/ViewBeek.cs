using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boris.Utils.Mvc.Attributes;
using Boris.BeekProject.Model.Beek;

namespace Boris.BeekProject.Guis.Shared.ViewModels{

    public class ViewBeek
    {
        [ScaffoldColumn(false)]
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("ISBN")]
        [Isbn(ErrorMessage = "Please enter a valid ISBN number")]
        public string Isbn { get; set; }

        [ScaffoldColumn(false)]
        [Required]
        public BeekTypes Type { get; set; }

        [ScaffoldColumn(false)]
        [UIHint("IsFiction")]
        [DisplayName("")]
        public bool IsFiction { get; set; }

        [ScaffoldColumn(false)]
        public string CoverArtPath { get; set; }

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
