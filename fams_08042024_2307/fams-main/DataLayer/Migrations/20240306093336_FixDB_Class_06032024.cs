using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixDBClass06032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("326ffd0c-33a6-447f-9e87-0b111c97684e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8046fe60-98e4-44c5-81d2-d386c6eed693"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e7aa6c31-d5f0-4c16-89bf-c301de749311"));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("48e5af83-0fa2-416b-b1b3-0ba8b23fb273"), null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6631), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6647), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("6bfff941-77a0-476a-8bb4-858b7a75fbe8"), null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6672), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6672), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("f84ed805-7c80-492c-9fd9-a49b2d935107"), null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6661), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6661), "Admin", "123@", 2, "0955422567", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("48e5af83-0fa2-416b-b1b3-0ba8b23fb273"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6bfff941-77a0-476a-8bb4-858b7a75fbe8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f84ed805-7c80-492c-9fd9-a49b2d935107"));

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Classes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("326ffd0c-33a6-447f-9e87-0b111c97684e"), null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7385), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7385), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("8046fe60-98e4-44c5-81d2-d386c6eed693"), null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7353), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7361), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("e7aa6c31-d5f0-4c16-89bf-c301de749311"), null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7375), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7376), "Admin", "123@", 2, "0955422567", 0 }
                });
        }
    }
}
