using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangePKTrainingContentV1030102024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingContents",
                table: "TrainingContents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0996d074-d8d7-49cf-af0a-73e4e9e1aee2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("2a638fb0-d2a1-4f82-ab34-a6baed2fa69a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("52bee380-a77f-42b4-8651-5be375f9a056"));

            migrationBuilder.AlterColumn<string>(
                name: "ContentId",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingContents",
                table: "TrainingContents",
                column: "ContentId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("09800e7c-d20b-44da-bd2c-dad58b039f0b"), null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5717), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5724), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("9f4c3b97-e053-4f6a-8901-998d2a7998b3"), null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5751), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5751), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("f99a1e61-e261-49d0-b973-a3fa133c46d2"), null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5744), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 1, 14, 51, 49, 911, DateTimeKind.Local).AddTicks(5745), "Admin", "123@", 2, "0955422567", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives",
                column: "Name",
                principalTable: "TrainingContents",
                principalColumn: "ContentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingContents",
                table: "TrainingContents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("09800e7c-d20b-44da-bd2c-dad58b039f0b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9f4c3b97-e053-4f6a-8901-998d2a7998b3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f99a1e61-e261-49d0-b973-a3fa133c46d2"));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContentId",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingContents",
                table: "TrainingContents",
                column: "Content");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("0996d074-d8d7-49cf-af0a-73e4e9e1aee2"), null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4275), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4275), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("2a638fb0-d2a1-4f82-ab34-a6baed2fa69a"), null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4253), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4265), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("52bee380-a77f-42b4-8651-5be375f9a056"), null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4281), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4281), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives",
                column: "Name",
                principalTable: "TrainingContents",
                principalColumn: "Content",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
