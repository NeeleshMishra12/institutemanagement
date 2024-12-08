using System.ComponentModel.DataAnnotations;

namespace Institutes_Managements.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string? Password { get; set; }

        [Required]
        [Phone]
        public string? ContactNumber { get; set; }

        [Required]
        public string? Role { get; set; } // Admin, Instructor, Student

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string? Address { get; set; }

        public int? CourseEnrolledId { get; set; } // Only for Students
    }
}
