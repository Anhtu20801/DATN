using Microsoft.EntityFrameworkCore;

namespace QLSV.Model.Models
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
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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