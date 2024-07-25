using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class TestV0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("57c005a0-b5ca-4d9a-87e8-836595f028b6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("af8acd56-2618-4fb3-8e0a-7bc8188e56bf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("fd006e94-81e3-4c96-8198-9e1a52bba517"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("14cde510-371a-44e2-975b-fdebb087d6f0"), null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(309), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(310), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("9db22591-bf69-4b41-a067-d93bda263214"), null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(300), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(301), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("a061d0fb-7fc1-4e48-be07-3d24a2f6c720"), null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(275), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(284), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("14cde510-371a-44e2-975b-fdebb087d6f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9db22591-bf69-4b41-a067-d93bda263214"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a061d0fb-7fc1-4e48-be07-3d24a2f6c720"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("57c005a0-b5ca-4d9a-87e8-836595f028b6"), null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(58), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(59), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("af8acd56-2618-4fb3-8e0a-7bc8188e56bf"), null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(69), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(70), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("fd006e94-81e3-4c96-8198-9e1a52bba517"), null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(35), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(45), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }
    }
}
