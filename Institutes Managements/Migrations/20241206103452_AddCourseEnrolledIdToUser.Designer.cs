﻿// <auto-generated />
using System;
using Institutes_Managements.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Institutes_Managements.Migrations
{
    [DbContext(typeof(InstituteDbcontext))]
    [Migration("20241206103452_AddCourseEnrolledIdToUser")]
    partial class AddCourseEnrolledIdToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Institutes_Managements.Models.Entities.Batch", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("BatchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BatchTiming")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Batchs");
                });

            modelBuilder.Entity("Institutes_Managements.Models.Entities.Course", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DurationWeeks")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseName = "Math 101",
                            Description = "Introduction to Mathematics"
                        },
                        new
                        {
                            Id = 2,
                            CourseName = "Physics 101",
                            Description = "Introduction to Physics"
                        });
                });

            modelBuilder.Entity("Institutes_Managements.Models.Entities.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseEnrolledId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseEnrolledId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Admin Address",
                            ContactNumber = "1234567890",
                            CourseEnrolledId = 1,
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@example.com",
                            FullName = "Admin User",
                            Password = "adminpassword",
                            Role = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Instructor Address",
                            ContactNumber = "0987654321",
                            CourseEnrolledId = 2,
                            DateOfBirth = new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "instructor@example.com",
                            FullName = "Instructor User",
                            Password = "instructorpassword",
                            Role = "instructor"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Student Address",
                            ContactNumber = "1122334455",
                            DateOfBirth = new DateTime(2000, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "student@example.com",
                            FullName = "Student User",
                            Password = "studentpassword",
                            Role = "student"
                        });
                });

            modelBuilder.Entity("Institutes_Managements.Models.Entities.Batch", b =>
                {
                    b.HasOne("Institutes_Managements.Models.Entities.Course", "Course")
                        .WithMany("Batches")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Institutes_Managements.Models.Entities.User", b =>
                {
                    b.HasOne("Institutes_Managements.Models.Entities.Course", "CourseEnrolled")
                        .WithMany()
                        .HasForeignKey("CourseEnrolledId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CourseEnrolled");
                });

            modelBuilder.Entity("Institutes_Managements.Models.Entities.Course", b =>
                {
                    b.Navigation("Batches");
                });
#pragma warning restore 612, 618
        }
    }
}
