using Institutes_Managements.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Institutes_Managements.Database
{
    public class InstituteDbcontext : DbContext
    {
        // Constructor to pass DbContextOptions
        public InstituteDbcontext(DbContextOptions<InstituteDbcontext> options)
            : base(options) { }

        // DbSet properties for your models
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Batch> Batchs { get; set; }

        // Optionally override OnModelCreating for more custom configurations like seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure foreign key relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.CourseEnrolled)
                .WithMany() // If there's no navigation property in Course
                .HasForeignKey(u => u.CourseEnrolledId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseName = "Math 101", Description = "Introduction to Mathematics" },
                new Course { Id = 2, CourseName = "Physics 101", Description = "Introduction to Physics" }
            );

            // Seed Users with CourseEnrolledId
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admin User",
                    Email = "admin@example.com",
                    Password = "adminpassword",
                    Role = "admin",
                    CourseEnrolledId = 1, // Assigning a valid CourseId
                    ContactNumber = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Address = "Admin Address"
                },
                new User
                {
                    Id = 2,
                    FullName = "Instructor User",
                    Email = "instructor@example.com",
                    Password = "instructorpassword",
                    Role = "instructor",
                    CourseEnrolledId = 2, // Assigning a valid CourseId
                    ContactNumber = "0987654321",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Address = "Instructor Address"
                },
                new User
                {
                    Id = 3,
                    FullName = "Student User",
                    Email = "student@example.com",
                    Password = "studentpassword",
                    Role = "student",
                    CourseEnrolledId = null, // Nullable for students not enrolled
                    ContactNumber = "1122334455",
                    DateOfBirth = new DateTime(2000, 10, 20),
                    Address = "Student Address"
                }
            );
        }


    }
}

