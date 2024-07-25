using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTrainingUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingUnits_Syllabuses_TopicCode",
                table: "TrainingUnits");

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

            migrationBuilder.AlterColumn<string>(
                name: "TopicCode",
                table: "TrainingUnits",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("307baad9-77b0-4074-bc1c-7dac066e1d08"), null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(824), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(825), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("6e2d628d-ca66-46f3-8ab1-96b7db2518a7"), null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(803), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(810), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("9dc18617-0fd2-44ac-b712-d91bac6a5db0"), null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(835), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 48, 27, 57, DateTimeKind.Local).AddTicks(835), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingUnits_Syllabuses_TopicCode",
                table: "TrainingUnits",
                column: "TopicCode",
                principalTable: "Syllabuses",
                principalColumn: "TopicCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingUnits_Syllabuses_TopicCode",
                table: "TrainingUnits");

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
                name: "TopicCode",
                table: "TrainingUnits",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("0a2e60bc-a801-42b0-8b68-9ed53d284af7"), null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2781), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2781), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("98a17d50-a0be-4a71-bb43-aabf5c491fd1"), null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2770), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2771), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("bb2d4e22-0576-4be3-a10e-042b75c925d8"), null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2732), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 1, 30, 13, 55, 50, 571, DateTimeKind.Local).AddTicks(2750), "Super Admin", "123@", 1, "0987788762", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingUnits_Syllabuses_TopicCode",
                table: "TrainingUnits",
                column: "TopicCode",
                principalTable: "Syllabuses",
                principalColumn: "TopicCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
