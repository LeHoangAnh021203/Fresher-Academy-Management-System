using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixDBV220032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("694bac14-bf25-4cda-bdb7-aa295258b526"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6a7acb39-345b-426d-a0bf-0371e2a308a6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a9fb966b-ce13-4cb6-a507-759504b9b023"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("61dd27cc-0e04-47c7-b199-e7f36140ef40"), null, new DateTime(2024, 3, 20, 11, 23, 10, 555, DateTimeKind.Local).AddTicks(7443), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 23, 10, 555, DateTimeKind.Local).AddTicks(7444), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("933fb012-7f18-4440-abf8-ce271dfcac40"), null, new DateTime(2024, 3, 20, 11, 23, 10, 555, DateTimeKind.Local).AddTicks(7408), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 23, 10, 555, DateTimeKind.Local).AddTicks(7418), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("c11a257d-02e8-4286-a5bf-68787d544e65"), null, new DateTime(2024, 3, 20, 11, 23, 10, 555, DateTimeKind.Local).AddTicks(7432), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 20, 11, 23, 10, 555, DateTimeKind.Local).AddTicks(7433), "Admin", "123@", 2, "0955422567", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("61dd27cc-0e04-47c7-b199-e7f36140ef40"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("933fb012-7f18-4440-abf8-ce271dfcac40"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c11a257d-02e8-4286-a5bf-68787d544e65"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("694bac14-bf25-4cda-bdb7-aa295258b526"), null, new DateTime(2024, 3, 20, 11, 16, 19, 683, DateTimeKind.Local).AddTicks(466), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 16, 19, 683, DateTimeKind.Local).AddTicks(474), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("6a7acb39-345b-426d-a0bf-0371e2a308a6"), null, new DateTime(2024, 3, 20, 11, 16, 19, 683, DateTimeKind.Local).AddTicks(497), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 16, 19, 683, DateTimeKind.Local).AddTicks(498), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("a9fb966b-ce13-4cb6-a507-759504b9b023"), null, new DateTime(2024, 3, 20, 11, 16, 19, 683, DateTimeKind.Local).AddTicks(487), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 20, 11, 16, 19, 683, DateTimeKind.Local).AddTicks(487), "Admin", "123@", 2, "0955422567", 0 }
                });
        }
    }
}
