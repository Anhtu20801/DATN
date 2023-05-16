using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class updateResultTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountStudent",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Classrooms");

            migrationBuilder.AddColumn<int>(
                name: "CountStudent",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Results",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Results",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 6, 16, 52, 9, 644, DateTimeKind.Local).AddTicks(7626));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountStudent",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Results");

            migrationBuilder.AddColumn<int>(
                name: "CountStudent",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Classrooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Classrooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 6, 16, 6, 2, 157, DateTimeKind.Local).AddTicks(5431));
        }
    }
}
