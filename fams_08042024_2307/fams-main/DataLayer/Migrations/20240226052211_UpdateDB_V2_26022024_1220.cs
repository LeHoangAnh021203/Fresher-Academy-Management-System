using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV2260220241220 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingContents_LearningObjectives_Code",
                table: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingContents_Code",
                table: "TrainingContents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c17db5e2-fed2-45f6-ad07-26f1cf26a396"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("daca6e4a-cb9d-4a65-8bed-2cb5fa53a26e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("df251c2b-7ddc-42b9-8025-193aa860c899"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LearningObjectives",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("2f42e333-2630-49b0-87da-650d3640c827"), null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4235), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4244), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("37296286-89f4-4433-806a-79c55e5bd3ec"), null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4258), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4259), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("b614fe59-0d98-463e-9dc1-7258a9ba159e"), null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4277), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4278), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningObjectives_Name",
                table: "LearningObjectives",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives",
                column: "Name",
                principalTable: "TrainingContents",
                principalColumn: "Content",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives");

            migrationBuilder.DropIndex(
                name: "IX_LearningObjectives_Name",
                table: "LearningObjectives");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("2f42e333-2630-49b0-87da-650d3640c827"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("37296286-89f4-4433-806a-79c55e5bd3ec"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b614fe59-0d98-463e-9dc1-7258a9ba159e"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LearningObjectives",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("c17db5e2-fed2-45f6-ad07-26f1cf26a396"), null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3065), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3065), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("daca6e4a-cb9d-4a65-8bed-2cb5fa53a26e"), null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3000), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3008), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("df251c2b-7ddc-42b9-8025-193aa860c899"), null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3075), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3076), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContents_Code",
                table: "TrainingContents",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingContents_LearningObjectives_Code",
                table: "TrainingContents",
                column: "Code",
                principalTable: "LearningObjectives",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
