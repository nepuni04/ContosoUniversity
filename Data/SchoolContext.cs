using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<ContosoUniversity.Models.Student> Students { get; set; }
        public DbSet<ContosoUniversity.Models.Enrollment> Enrollments { get; set; }
        public DbSet<ContosoUniversity.Models.Course> Courses { get; set; }
        public DbSet<ContosoUniversity.Models.Department> Departments { get; set; }
        public DbSet<ContosoUniversity.Models.Instructor> Instructors { get; set; }
        public DbSet<ContosoUniversity.Models.OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<ContosoUniversity.Models.CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });

            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.CourseID, e.StudentID });
        }

    }
}
