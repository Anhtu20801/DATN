﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLSV.Data;

#nullable disable

namespace QLSV.Data.Migrations
{
    [DbContext(typeof(StudentDBContext))]
    [Migration("20230414161907_initialdata")]
    partial class initialdata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QLSV.Model.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"), 1L, 1);

                    b.Property<DateTime>("AttenTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Check")
                        .HasColumnType("bit");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("AttendanceId");

                    b.HasIndex("StudentId", "TeacherId", "CourseId");

                    b.ToTable("Attendances");

                    b.HasData(
                        new
                        {
                            AttendanceId = 1,
                            AttenTime = new DateTime(2023, 4, 14, 23, 19, 6, 963, DateTimeKind.Local).AddTicks(6029),
                            Check = true,
                            CourseId = 1,
                            StudentId = 1,
                            TeacherId = 1
                        },
                        new
                        {
                            AttendanceId = 2,
                            AttenTime = new DateTime(2023, 4, 14, 23, 19, 6, 963, DateTimeKind.Local).AddTicks(6045),
                            Check = true,
                            CourseId = 1,
                            StudentId = 1,
                            TeacherId = 1
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Classroom", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lesson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("TeacherId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Classrooms");

                    b.HasData(
                        new
                        {
                            TeacherId = 1,
                            CourseId = 1,
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Lesson = "1,2,3",
                            Semester = 5,
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Documentation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamForm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PracticeCreditsNumber")
                        .HasColumnType("float");

                    b.Property<string>("SpecialRequirements")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TheoryCreditsNumber")
                        .HasColumnType("float");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            Name = "ASP.NET",
                            PracticeCreditsNumber = 1.0,
                            TheoryCreditsNumber = 2.0
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            Description = "Khoa công nghệ thông tin",
                            Name = "CNTT"
                        },
                        new
                        {
                            DepartmentId = 2,
                            Description = "Khoa quản trị kinh doanh",
                            Name = "QTKD"
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.PrimaryClass", b =>
                {
                    b.Property<int>("PrimaryClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrimaryClassId"), 1L, 1);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PrimaryClassId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("PrimaryClasses");

                    b.HasData(
                        new
                        {
                            PrimaryClassId = 1,
                            DepartmentId = 1,
                            Name = "KTPM01"
                        },
                        new
                        {
                            PrimaryClassId = 2,
                            DepartmentId = 1,
                            Name = "CNTT01"
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Result", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("FinalMark")
                        .HasColumnType("int");

                    b.Property<int?>("MitermMark")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RegularMark")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Results");

                    b.HasData(
                        new
                        {
                            TeacherId = 1,
                            CourseId = 1,
                            StudentId = 1
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("BackCCCDImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("CCCDDateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("CCCDNumber")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime?>("DateOfAdmission")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("FrontCCCDImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("MedicareCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("PrimaryClassId")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.HasIndex("PrimaryClassId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            Address = "Đông Anh - Hà Nội",
                            Gender = "Nam",
                            Major = "KTPM",
                            Name = "Nguyễn Anh Tú",
                            PhoneNumber = "0966229562",
                            PrimaryClassId = 1
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            TeacherId = 1,
                            DepartmentId = 1,
                            Name = "Nguyễn Thị Hương Lan"
                        });
                });

            modelBuilder.Entity("QLSV.Model.Models.Attendance", b =>
                {
                    b.HasOne("QLSV.Model.Models.Result", "Results")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId", "TeacherId", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Results");
                });

            modelBuilder.Entity("QLSV.Model.Models.Classroom", b =>
                {
                    b.HasOne("QLSV.Model.Models.Course", "Courses")
                        .WithMany("Classrooms")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QLSV.Model.Models.Teacher", "Teachers")
                        .WithMany("Classrooms")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courses");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("QLSV.Model.Models.PrimaryClass", b =>
                {
                    b.HasOne("QLSV.Model.Models.Department", "Department")
                        .WithMany("PrimaryClasses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("QLSV.Model.Models.Result", b =>
                {
                    b.HasOne("QLSV.Model.Models.Student", "Student")
                        .WithMany("Results")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QLSV.Model.Models.Classroom", "Classroom")
                        .WithMany("Results")
                        .HasForeignKey("TeacherId", "CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("QLSV.Model.Models.Student", b =>
                {
                    b.HasOne("QLSV.Model.Models.PrimaryClass", "PrimaryClass")
                        .WithMany("Students")
                        .HasForeignKey("PrimaryClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrimaryClass");
                });

            modelBuilder.Entity("QLSV.Model.Models.Teacher", b =>
                {
                    b.HasOne("QLSV.Model.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("QLSV.Model.Models.Classroom", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("QLSV.Model.Models.Course", b =>
                {
                    b.Navigation("Classrooms");
                });

            modelBuilder.Entity("QLSV.Model.Models.Department", b =>
                {
                    b.Navigation("PrimaryClasses");
                });

            modelBuilder.Entity("QLSV.Model.Models.PrimaryClass", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("QLSV.Model.Models.Result", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("QLSV.Model.Models.Student", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("QLSV.Model.Models.Teacher", b =>
                {
                    b.Navigation("Classrooms");
                });
#pragma warning restore 612, 618
        }
    }
}
