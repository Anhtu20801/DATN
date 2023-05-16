using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class updateClassroomTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 6, 22, 18, 26, 379, DateTimeKind.Local).AddTicks(9224));

            migrationBuilder.UpdateData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: 1,
                column: "Name",
                value: "IT6000.1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Classrooms");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 6, 21, 32, 49, 150, DateTimeKind.Local).AddTicks(1100));
        }
    }
}
