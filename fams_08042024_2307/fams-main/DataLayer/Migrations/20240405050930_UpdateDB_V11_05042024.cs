using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV1105042024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5a5dc416-4214-44a0-aa30-35179a143e25"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b9834803-6b36-49a5-bde1-2b664f30eb11"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("cda6f292-29a9-4e23-bf07-f71e01eea284"));

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "TrainingCalendars");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TrainingCalendars");

            migrationBuilder.AddColumn<string>(
                name: "Admin",
                table: "TrainingCalendars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "TrainingCalendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "TrainingCalendars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Trainer",
                table: "TrainingCalendars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("067db6be-b9b1-4893-a192-cf7f8ff2fcd4"), null, new DateTime(2024, 4, 5, 12, 9, 30, 361, DateTimeKind.Local).AddTicks(2177), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 5, 12, 9, 30, 361, DateTimeKind.Local).AddTicks(2177), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("5aee82c6-25a5-4361-8b4c-635a1a121ce7"), null, new DateTime(2024, 4, 5, 12, 9, 30, 361, DateTimeKind.Local).AddTicks(2162), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 5, 12, 9, 30, 361, DateTimeKind.Local).AddTicks(2163), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("a4dad951-2124-4d8b-a563-35597d689fcb"), null, new DateTime(2024, 4, 5, 12, 9, 30, 361, DateTimeKind.Local).AddTicks(2145), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 5, 12, 9, 30, 361, DateTimeKind.Local).AddTicks(2152), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("067db6be-b9b1-4893-a192-cf7f8ff2fcd4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5aee82c6-25a5-4361-8b4c-635a1a121ce7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a4dad951-2124-4d8b-a563-35597d689fcb"));

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "TrainingCalendars");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TrainingCalendars");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "TrainingCalendars");

            migrationBuilder.DropColumn(
                name: "Trainer",
                table: "TrainingCalendars");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "TrainingCalendars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TrainingCalendars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("5a5dc416-4214-44a0-aa30-35179a143e25"), null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9643), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9644), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("b9834803-6b36-49a5-bde1-2b664f30eb11"), null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9633), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9634), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("cda6f292-29a9-4e23-bf07-f71e01eea284"), null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9610), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9618), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }
    }
}
