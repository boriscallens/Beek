using System.ComponentModel.DataAnnotations;

namespace Boris.BeekProject.Guis.Shared.ViewModels
{
    public class ViewUser
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Retyped password required")]
        public string PasswordRepeat { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mail is required")]
        /*src: http://fightingforalostcause.net/misc/2006/compare-email-regex.php*/
        [RegularExpression(@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-z0-9]{1}[a-z0-9\-]{0,62}[a-z0-9]{1})|[a-z])\.)+[a-z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"
                           ,ErrorMessage = "Valid email required")]
        public string Email { get; set; }

        public bool ArePasswordsEqual()
        {
            return !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(PasswordRepeat) &&
                   Password.Equals(PasswordRepeat);
        }

    }
}