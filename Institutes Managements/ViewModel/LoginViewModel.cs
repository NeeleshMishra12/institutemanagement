using System.ComponentModel.DataAnnotations;

namespace Institutes_Managements.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }
    }

}
