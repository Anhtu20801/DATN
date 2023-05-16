using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QLSV.Data.Data;
using QLSV.Model.Models;

namespace QLSV.Data
{
    public class StudentDBContext: IdentityDbContext
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
                .HasKey(cl => cl.ClassroomId);

            modelBuilder.Entity<Result>()
                .HasKey(r => new { r.ClassroomId, r.StudentId });

            modelBuilder.Entity<Result>()
                .HasOne<Student>(s => s.Student)
                .WithMany(r => r.Results)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne<Classroom>(classroom => classroom.Classroom)
                .WithMany(r => r.Results)
                .HasForeignKey(classroom =>  classroom.ClassroomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne<Result>(r => r.Results)
                .WithMany(a => a.Attendances)
                .HasForeignKey(r => new {r.StudentId, r.ClassroomId});
            
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        //entities
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<PrimaryClass> PrimaryClasses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}