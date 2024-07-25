using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DBLASTFIX20032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("68505484-4d49-440d-b10e-3d0519b676a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9c082052-a13b-466c-95c2-bb51402194f1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bb52b4e9-9898-4495-a7ee-61b5a795be55"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("760217a7-715f-4361-99e1-b839d28b55fb"), null, new DateTime(2024, 3, 20, 11, 44, 30, 651, DateTimeKind.Local).AddTicks(3552), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 44, 30, 651, DateTimeKind.Local).AddTicks(3571), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("7720af52-24d5-49f9-8970-d237a06b554b"), null, new DateTime(2024, 3, 20, 11, 44, 30, 651, DateTimeKind.Local).AddTicks(3589), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 20, 11, 44, 30, 651, DateTimeKind.Local).AddTicks(3590), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("aab0cc73-1e9c-46e2-8072-8f48ae011412"), null, new DateTime(2024, 3, 20, 11, 44, 30, 651, DateTimeKind.Local).AddTicks(3599), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 44, 30, 651, DateTimeKind.Local).AddTicks(3600), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("760217a7-715f-4361-99e1-b839d28b55fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("7720af52-24d5-49f9-8970-d237a06b554b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("aab0cc73-1e9c-46e2-8072-8f48ae011412"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("68505484-4d49-440d-b10e-3d0519b676a8"), null, new DateTime(2024, 3, 20, 11, 43, 49, 754, DateTimeKind.Local).AddTicks(1048), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 20, 11, 43, 49, 754, DateTimeKind.Local).AddTicks(1049), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("9c082052-a13b-466c-95c2-bb51402194f1"), null, new DateTime(2024, 3, 20, 11, 43, 49, 754, DateTimeKind.Local).AddTicks(1018), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 43, 49, 754, DateTimeKind.Local).AddTicks(1033), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("bb52b4e9-9898-4495-a7ee-61b5a795be55"), null, new DateTime(2024, 3, 20, 11, 43, 49, 754, DateTimeKind.Local).AddTicks(1059), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 43, 49, 754, DateTimeKind.Local).AddTicks(1060), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }
    }
}
