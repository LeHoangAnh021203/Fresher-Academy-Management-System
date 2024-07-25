using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataUserUserPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserPermissions",
                columns: new[] { "PermissionId", "Class", "LearningMaterial", "PermissionName", "Syllabus", "TrainingProgram", "UserManagement", "Version" },
                values: new object[,]
                {
                    { 1, 4, 4, "Super Admin", 4, 4, 4, 0 },
                    { 2, 4, 4, "Admin", 4, 4, 0, 0 },
                    { 3, 1, 2, "Trainer", 2, 1, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"), null, new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4981), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4981), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"), null, new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4969), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4969), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"), null, new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4910), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 1, 29, 12, 33, 22, 913, DateTimeKind.Local).AddTicks(4918), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a6eba0dd-14cc-4a2b-8af9-39ede88f9787"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("df1756fa-9f21-4a47-8ff8-7c7d9ed54553"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e7d9a2dd-cafc-4303-af6e-0c8331d9ab72"));

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserPermissions",
                keyColumn: "PermissionId",
                keyValue: 3);
        }
    }
}
