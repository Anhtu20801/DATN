using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class Add_Value_For_Attendance_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 5, 12, 58, 46, 620, DateTimeKind.Local).AddTicks(5179));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 5, 1, 31, 30, 939, DateTimeKind.Local).AddTicks(8262));

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceId", "AttenTime", "Check", "ClassroomId", "Note", "StudentId" },
                values: new object[] { 2, new DateTime(2023, 5, 5, 1, 31, 30, 939, DateTimeKind.Local).AddTicks(8275), true, 1, null, 1 });
        }
    }
}
