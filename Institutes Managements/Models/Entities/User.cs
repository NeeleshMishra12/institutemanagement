namespace Institutes_Managements.Models.Entities
{
    public class User
    {
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } // Admin, Instructor, Student
        public string? ContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public Course? CourseEnrolled { get; set; } // Navigation property
        public int? CourseEnrolledId { get; set; } // Made nullable

    }

}
