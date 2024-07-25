using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV618032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TrainingPrograms_TrainingProgramCode",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TrainingProgramCode",
                table: "Classes");

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

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramStatus",
                table: "TrainingPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "TrainingContents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TrainingProgramCode",
                table: "Classes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "Classes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ClassCode",
                table: "Classes",
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
                    { new Guid("08fbca18-1e2f-4733-88a1-f8bcd3405815"), null, new DateTime(2024, 3, 18, 14, 40, 24, 620, DateTimeKind.Local).AddTicks(31), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 18, 14, 40, 24, 620, DateTimeKind.Local).AddTicks(32), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("32ef3d52-1169-4079-9fbd-6ed699148c32"), null, new DateTime(2024, 3, 18, 14, 40, 24, 620, DateTimeKind.Local).AddTicks(21), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 18, 14, 40, 24, 620, DateTimeKind.Local).AddTicks(22), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("6157b81b-49b4-4add-a930-824fb641e3cc"), null, new DateTime(2024, 3, 18, 14, 40, 24, 619, DateTimeKind.Local).AddTicks(9997), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 18, 14, 40, 24, 620, DateTimeKind.Local).AddTicks(6), "Super Admin", "123@", 1, "0987788762", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TrainingProgramCode",
                table: "Classes",
                column: "TrainingProgramCode",
                unique: true,
                filter: "[TrainingProgramCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TrainingPrograms_TrainingProgramCode",
                table: "Classes",
                column: "TrainingProgramCode",
                principalTable: "TrainingPrograms",
                principalColumn: "TrainingProgramCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TrainingPrograms_TrainingProgramCode",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingContents_LearningObjectives_Code",
                table: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingContents_Code",
                table: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_Classes_TrainingProgramCode",
                table: "Classes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("08fbca18-1e2f-4733-88a1-f8bcd3405815"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("32ef3d52-1169-4079-9fbd-6ed699148c32"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6157b81b-49b4-4add-a930-824fb641e3cc"));

            migrationBuilder.DropColumn(
                name: "TrainingProgramStatus",
                table: "TrainingPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "TrainingContents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TrainingProgramCode",
                table: "Classes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClassName",
                table: "Classes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClassCode",
                table: "Classes",
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
                    { new Guid("48e5af83-0fa2-416b-b1b3-0ba8b23fb273"), null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6631), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6647), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("6bfff941-77a0-476a-8bb4-858b7a75fbe8"), null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6672), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6672), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("f84ed805-7c80-492c-9fd9-a49b2d935107"), null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6661), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 6, 16, 33, 36, 101, DateTimeKind.Local).AddTicks(6661), "Admin", "123@", 2, "0955422567", 0 }
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TrainingProgramCode",
                table: "Classes",
                column: "TrainingProgramCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TrainingPrograms_TrainingProgramCode",
                table: "Classes",
                column: "TrainingProgramCode",
                principalTable: "TrainingPrograms",
                principalColumn: "TrainingProgramCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
