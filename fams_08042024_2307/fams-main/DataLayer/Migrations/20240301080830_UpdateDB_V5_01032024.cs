using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV501032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("09800e7c-d20b-44da-bd2c-dad58b039f0b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9f4c3b97-e053-4f6a-8901-998d2a7998b3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f99a1e61-e261-49d0-b973-a3fa133c46d2"));

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "TrainingContents",
                newName: "ContentName");

            migrationBuilder.AlterColumn<string>(
                name: "ContentName",
                table: "TrainingContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("7e1d7921-9162-42c6-b6b5-b4ad3fdedc96"), null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4130), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4130), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("a9bde18c-f82c-452c-a0e8-5d862b2a1ec3"), null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4107), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4115), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("c3f677ad-638e-43bb-ac0a-0bcdb97ed0da"), null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4124), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4124), "Admin", "123@", 2, "0955422567", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("7e1d7921-9162-42c6-b6b5-b4ad3fdedc96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a9bde18c-f82c-452c-a0e8-5d862b2a1ec3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c3f677ad-638e-43bb-ac0a-0bcdb97ed0da"));

            migrationBuilder.RenameColumn(
                name: "ContentName",
                table: "TrainingContents",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("09800e7c-d20b-44da-bd2c-dad58b039f0b"), null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5717), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5724), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("9f4c3b97-e053-4f6a-8901-998d2a7998b3"), null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5751), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5751), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("f99a1e61-e261-49d0-b973-a3fa133c46d2"), null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5744), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5745), "Admin", "123@", 2, "0955422567", 0 }
                });
        }
    }
}
