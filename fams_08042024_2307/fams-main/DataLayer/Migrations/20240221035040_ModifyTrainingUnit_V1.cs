using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTrainingUnitV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("307baad9-77b0-4074-bc1c-7dac066e1d08"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6e2d628d-ca66-46f3-8ab1-96b7db2518a7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9dc18617-0fd2-44ac-b712-d91bac6a5db0"));

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "TrainingUnits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("0a7ddb12-ff95-4c85-bbe4-f1b583f2bf52"), null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9249), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9250), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("209bac26-fbc3-486f-9f7f-96cf4246990c"), null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9259), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9259), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("533b3c28-f302-4c86-a252-1ed7e427c218"), null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9226), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9236), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0a7ddb12-ff95-4c85-bbe4-f1b583f2bf52"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("209bac26-fbc3-486f-9f7f-96cf4246990c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("533b3c28-f302-4c86-a252-1ed7e427c218"));

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "TrainingUnits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("307baad9-77b0-4074-bc1c-7dac066e1d08"), null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(824), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(825), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("6e2d628d-ca66-46f3-8ab1-96b7db2518a7"), null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(803), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(810), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("9dc18617-0fd2-44ac-b712-d91bac6a5db0"), null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(835), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(835), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }
    }
}
