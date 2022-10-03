using System.ComponentModel.DataAnnotations;

namespace Crm.Api.Prospects
{
    public class RegisterProspectRequest
    {
        [StringLength(80, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 10)]
        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(80, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(80, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 4)]
        [Required(ErrorMessage = "Country Required")]
        [DataType(DataType.Text)]
        public string Country { get; set; }
    }
}
