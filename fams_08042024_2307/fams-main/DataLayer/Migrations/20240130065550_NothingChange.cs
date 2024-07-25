using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class NothingChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("128c6965-8b76-4801-a9ab-876166c6055d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6e1ebef0-4174-4f18-bfde-02c98b8f2964"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9d5caf27-8bba-4af9-b1a3-f32e97929943"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("0a2e60bc-a801-42b0-8b68-9ed53d284af7"), null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2781), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2781), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("98a17d50-a0be-4a71-bb43-aabf5c491fd1"), null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2770), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2771), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("bb2d4e22-0576-4be3-a10e-042b75c925d8"), null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2732), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2750), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0a2e60bc-a801-42b0-8b68-9ed53d284af7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("98a17d50-a0be-4a71-bb43-aabf5c491fd1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bb2d4e22-0576-4be3-a10e-042b75c925d8"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("128c6965-8b76-4801-a9ab-876166c6055d"), null, new DateTime(2024, 1, 30, 13, 53, 3, 785, DateTimeKind.Local).AddTicks(3323), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 1, 30, 13, 53, 3, 785, DateTimeKind.Local).AddTicks(3334), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("6e1ebef0-4174-4f18-bfde-02c98b8f2964"), null, new DateTime(2024, 1, 30, 13, 53, 3, 785, DateTimeKind.Local).AddTicks(3356), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 1, 30, 13, 53, 3, 785, DateTimeKind.Local).AddTicks(3357), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("9d5caf27-8bba-4af9-b1a3-f32e97929943"), null, new DateTime(2024, 1, 30, 13, 53, 3, 785, DateTimeKind.Local).AddTicks(3346), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 1, 30, 13, 53, 3, 785, DateTimeKind.Local).AddTicks(3347), "Admin", "123@", 2, "0955422567", 0 }
                });
        }
    }
}
