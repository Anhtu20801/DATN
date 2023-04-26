using Microsoft.EntityFrameworkCore;
using QLSV.Model.Models;

namespace QLSV.Data.Data
{
    public static class Initializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department() { DepartmentId = 1, Name = "CNTT", Description = "Khoa công nghệ thông tin" },
                new Department() { DepartmentId = 2, Name = "QTKD", Description = "Khoa quản trị kinh doanh" }
            );

            modelBuilder.Entity<PrimaryClass>().HasData(
                new PrimaryClass()
                {
                    PrimaryClassId= 1,
                    Name = "KTPM01",
                    DepartmentId = 1
                },
                new PrimaryClass()
                {
                    PrimaryClassId = 2,
                    Name = "CNTT01",
                    DepartmentId = 1
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student() 
                {
                    StudentId = 1,
                    StudentCode = "2019601375",
                    Name = "Nguyễn Anh Tú",
                    Gender = "Nam",
                    Address = "Đông Anh - Hà Nội",
                    Major = "KTPM",
                    PhoneNumber = "0966229562",
                    PrimaryClassId= 1
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course() 
                {
                    CourseId = 1,
                    Name = "ASP.NET",
                    TheoryCreditsNumber = 2,
                    PracticeCreditsNumber= 1
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher() 
                { 
                    TeacherId = 1,
                    TeacherCode = "2009601375",
                    Name = "Nguyễn Thị Hương Lan",
                    DepartmentId = 1
                }
            );

            modelBuilder.Entity<Classroom>().HasData(
                new Classroom() 
                {
                    TeacherId= 1,
                    CourseId= 1,
                    Semester = 5,
                    Lesson = "1,2,3"
                }
            );

            modelBuilder.Entity<Result>().HasData(
                new Result()
                {
                    StudentId = 1,
                    TeacherId = 1,
                    CourseId = 1
                }
            );

            modelBuilder.Entity<Attendance>().HasData(
                new Attendance() 
                {
                    AttendanceId = 1,
                    StudentId = 1, 
                    TeacherId = 1,
                    CourseId= 1,
                    Check = true
                },
                new Attendance()
                {
                    AttendanceId = 2,
                    StudentId = 1,
                    TeacherId = 1,
                    CourseId = 1,
                    Check = true
                }
            );
        }
    }
}