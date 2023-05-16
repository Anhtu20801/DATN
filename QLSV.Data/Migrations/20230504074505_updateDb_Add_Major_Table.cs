using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Data.Migrations
{
    public partial class updateDb_Add_Major_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryClasses_Departments_DepartmentId",
                table: "PrimaryClasses");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "PrimaryClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "PrimaryClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorId);
                    table.ForeignKey(
                        name: "FK_Majors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 5, 4, 14, 45, 3, 964, DateTimeKind.Local).AddTicks(6104));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2,
                column: "AttenTime",
                value: new DateTime(2023, 5, 4, 14, 45, 3, 964, DateTimeKind.Local).AddTicks(6120));

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorId", "DepartmentId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Ngành công nghệ thông tin", "CNTT" },
                    { 2, 1, "Ngành kỹ thuật phần mềm", "KTPM" }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "PrimaryClassId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "PrimaryClasses",
                keyColumn: "PrimaryClassId",
                keyValue: 1,
                columns: new[] { "DepartmentId", "MajorId", "Name" },
                values: new object[] { null, 1, "CNTT01" });

            migrationBuilder.UpdateData(
                table: "PrimaryClasses",
                keyColumn: "PrimaryClassId",
                keyValue: 2,
                columns: new[] { "DepartmentId", "MajorId", "Name" },
                values: new object[] { null, 2, "KTPM01" });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryClasses_MajorId",
                table: "PrimaryClasses",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_DepartmentId",
                table: "Majors",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryClasses_Departments_DepartmentId",
                table: "PrimaryClasses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryClasses_Majors_MajorId",
                table: "PrimaryClasses",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryClasses_Departments_DepartmentId",
                table: "PrimaryClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_PrimaryClasses_Majors_MajorId",
                table: "PrimaryClasses");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryClasses_MajorId",
                table: "PrimaryClasses");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "PrimaryClasses");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Students",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "PrimaryClasses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 1,
                column: "AttenTime",
                value: new DateTime(2023, 4, 27, 22, 39, 22, 484, DateTimeKind.Local).AddTicks(8389));

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 2,
                column: "AttenTime",
                value: new DateTime(2023, 4, 27, 22, 39, 22, 484, DateTimeKind.Local).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "PrimaryClasses",
                keyColumn: "PrimaryClassId",
                keyValue: 1,
                columns: new[] { "DepartmentId", "Name" },
                values: new object[] { 1, "KTPM01" });

            migrationBuilder.UpdateData(
                table: "PrimaryClasses",
                keyColumn: "PrimaryClassId",
                keyValue: 2,
                columns: new[] { "DepartmentId", "Name" },
                values: new object[] { 1, "CNTT01" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                columns: new[] { "Major", "PrimaryClassId" },
                values: new object[] { "KTPM", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryClasses_Departments_DepartmentId",
                table: "PrimaryClasses",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
