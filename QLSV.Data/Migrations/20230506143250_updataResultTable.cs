using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class updataResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountStudent",
                table: "Results");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 6, 21, 32, 49, 150, DateTimeKind.Local).AddTicks(1100));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountStudent",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 6, 16, 52, 9, 644, DateTimeKind.Local).AddTicks(7626));
        }
    }
}
