using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class updatedatabase_Classroom_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Results_StudentId_TeacherId_CourseId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Classrooms_TeacherId_CourseId",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classrooms",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId_TeacherId_CourseId",
                table: "Attendances");

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumns: new[] { "CourseId", "TeacherId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Results",
                newName: "ClassroomId");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Attendances",
                newName: "ClassroomId");

            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                columns: new[] { "ClassroomId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classrooms",
                table: "Classrooms",
                column: "ClassroomId");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 5, 1, 31, 30, 939, DateTimeKind.Local).AddTicks(8262));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2,
                column: "AttenTime",
                value: new DateTime(2023, 5, 5, 1, 31, 30, 939, DateTimeKind.Local).AddTicks(8275));

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "CourseId", "EndTime", "Lesson", "Semester", "StartTime", "TeacherId" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1,2,3", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId_ClassroomId",
                table: "Attendances",
                columns: new[] { "StudentId", "ClassroomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Results_StudentId_ClassroomId",
                table: "Attendances",
                columns: new[] { "StudentId", "ClassroomId" },
                principalTable: "Results",
                principalColumns: new[] { "ClassroomId", "StudentId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Classrooms_ClassroomId",
                table: "Results",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ClassroomId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Results_StudentId_ClassroomId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Classrooms_ClassroomId",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Classrooms",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId_ClassroomId",
                table: "Attendances");

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumns: new[] { "ClassroomId", "StudentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Classrooms");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "Results",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "Attendances",
                newName: "TeacherId");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                columns: new[] { "TeacherId", "CourseId", "StudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Classrooms",
                table: "Classrooms",
                columns: new[] { "TeacherId", "CourseId" });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "CourseId", "StudentId", "TeacherId", "FinalMark", "MitermMark", "Note", "RegularMark" },
                values: new object[] { 1, 1, 1, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                columns: new[] { "AttenTime", "CourseId" },
                values: new object[] { new DateTime(2023, 5, 4, 14, 45, 3, 964, DateTimeKind.Local).AddTicks(6104), 1 });

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2,
                columns: new[] { "AttenTime", "CourseId" },
                values: new object[] { new DateTime(2023, 5, 4, 14, 45, 3, 964, DateTimeKind.Local).AddTicks(6120), 1 });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Classrooms_TeacherId_CourseId",
                table: "Results",
                columns: new[] { "TeacherId", "CourseId" },
                principalTable: "Classrooms",
                principalColumns: new[] { "TeacherId", "CourseId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
