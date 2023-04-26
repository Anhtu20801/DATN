using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class initialdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Results_ResultsTeacherId_ResultsCourseId_ResultsStudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ResultsTeacherId_ResultsCourseId_ResultsStudentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ResultsCourseId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ResultsStudentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ResultsTeacherId",
                table: "Attendances");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "FrontCCCDImage",
                table: "Students",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAdmission",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CCCDNumber",
                table: "Students",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CCCDDateStart",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<byte[]>(
                name: "BackCCCDImage",
                table: "Students",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "Students",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Content", "Description", "Documentation", "ExamForm", "Name", "Note", "PracticeCreditsNumber", "SpecialRequirements", "TheoryCreditsNumber" },
                values: new object[] { 1, null, null, null, null, "ASP.NET", null, 1.0, null, 2.0 });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Description", "Name" },
                values: new object[] { 1, "Khoa công nghệ thông tin", "CNTT" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Description", "Name" },
                values: new object[] { 2, "Khoa quản trị kinh doanh", "QTKD" });

            migrationBuilder.InsertData(
                table: "PrimaryClasses",
                columns: new[] { "PrimaryClassId", "DepartmentId", "Description", "Name" },
                values: new object[] { 1, 1, null, "KTPM01" });

            migrationBuilder.InsertData(
                table: "PrimaryClasses",
                columns: new[] { "PrimaryClassId", "DepartmentId", "Description", "Name" },
                values: new object[] { 2, 1, null, "CNTT01" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "Address", "DepartmentId", "Name", "PhoneNumber" },
                values: new object[] { 1, null, 1, "Nguyễn Thị Hương Lan", null });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "CourseId", "TeacherId", "EndTime", "Lesson", "Semester", "StartTime" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1,2,3", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Address", "Avatar", "BackCCCDImage", "CCCDDateStart", "CCCDNumber", "DateOfAdmission", "DateOfBirth", "FrontCCCDImage", "Gender", "Major", "MedicareCardNumber", "Name", "PhoneNumber", "PrimaryClassId" },
                values: new object[] { 1, "Đông Anh - Hà Nội", null, null, null, null, null, null, null, "Nam", "KTPM", null, "Nguyễn Anh Tú", "0966229562", 1 });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "CourseId", "StudentId", "TeacherId", "FinalMark", "MitermMark", "Note", "RegularMark" },
                values: new object[] { 1, 1, 1, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceId", "AttenTime", "Check", "CourseId", "Note", "StudentId", "TeacherId" },
                values: new object[] { 1, new DateTime(2023, 4, 14, 23, 19, 6, 963, DateTimeKind.Local).AddTicks(6029), true, 1, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceId", "AttenTime", "Check", "CourseId", "Note", "StudentId", "TeacherId" },
                values: new object[] { 2, new DateTime(2023, 4, 14, 23, 19, 6, 963, DateTimeKind.Local).AddTicks(6045), true, 1, null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId_TeacherId_CourseId",
                table: "Attendances",
                columns: new[] { "StudentId", "TeacherId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Results_StudentId_TeacherId_CourseId",
                table: "Attendances",
                columns: new[] { "StudentId", "TeacherId", "CourseId" },
                principalTable: "Results",
                principalColumns: new[] { "TeacherId", "CourseId", "StudentId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Results_StudentId_TeacherId_CourseId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId_TeacherId_CourseId",
                table: "Attendances");

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrimaryClasses",
                keyColumn: "PrimaryClassId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumns: new[] { "CourseId", "StudentId", "TeacherId" },
                keyValues: new object[] { 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PrimaryClasses",
                keyColumn: "PrimaryClassId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FrontCCCDImage",
                table: "Students",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAdmission",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CCCDNumber",
                table: "Students",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CCCDDateStart",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "BackCCCDImage",
                table: "Students",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "Students",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ResultsCourseId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResultsStudentId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResultsTeacherId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ResultsTeacherId_ResultsCourseId_ResultsStudentId",
                table: "Attendances",
                columns: new[] { "ResultsTeacherId", "ResultsCourseId", "ResultsStudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Results_ResultsTeacherId_ResultsCourseId_ResultsStudentId",
                table: "Attendances",
                columns: new[] { "ResultsTeacherId", "ResultsCourseId", "ResultsStudentId" },
                principalTable: "Results",
                principalColumns: new[] { "TeacherId", "CourseId", "StudentId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
