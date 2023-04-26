using Microsoft.EntityFrameworkCore;
using QLSV.Data.Data;
using QLSV.Model.Models;

namespace QLSV.Data
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext()
        {
        }

        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>()
                .HasKey(classroom => new { classroom.TeacherId, classroom.CourseId });

            modelBuilder.Entity<Result>()
                .HasKey(r => new { r.TeacherId, r.CourseId,r.StudentId });

            modelBuilder.Entity<Result>()
                .HasOne<Student>(s => s.Student)
                .WithMany(r => r.Results)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne<Classroom>(classroom => classroom.Classroom)
                .WithMany(r => r.Results)
                .HasForeignKey(classroom => new { classroom.TeacherId, classroom.CourseId})
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne<Result>(r => r.Results)
                .WithMany(a => a.Attendances)
                .HasForeignKey(r => new {r.StudentId, r.TeacherId, r.CourseId});
            
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        //entities
        public DbSet<Department> Departments { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<PrimaryClass> PrimaryClasses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}