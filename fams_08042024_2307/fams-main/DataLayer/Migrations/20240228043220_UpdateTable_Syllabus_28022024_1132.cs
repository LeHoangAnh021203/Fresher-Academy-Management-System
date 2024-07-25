using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableSyllabus280220241132 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingContents_TrainingUnits_UnitCode",
                table: "TrainingContents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("51e5d305-4322-44f2-9989-dac1206924e7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("6384f22b-9a49-456a-8716-a63d500086b7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a6f1d28b-9f14-4aeb-a27b-b2784c943487"));

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "TrainingFormat",
                table: "TrainingContents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "AssessmentID",
                table: "Syllabuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseObjective",
                table: "Syllabuses",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TechnicalRequirement",
                table: "Syllabuses",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("326962b0-d21b-4df2-96e3-64677295d4fd"), null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9822), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9831), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("ba1903b1-47a2-485a-8b36-a85b3e1557fd"), null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9844), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9845), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("df1efe50-04c0-45ff-ace0-2e525df6c0f8"), null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9856), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9857), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingContents_TrainingUnits_UnitCode",
                table: "TrainingContents",
                column: "UnitCode",
                principalTable: "TrainingUnits",
                principalColumn: "UnitCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingContents_TrainingUnits_UnitCode",
                table: "TrainingContents");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("326962b0-d21b-4df2-96e3-64677295d4fd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ba1903b1-47a2-485a-8b36-a85b3e1557fd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("df1efe50-04c0-45ff-ace0-2e525df6c0f8"));

            migrationBuilder.DropColumn(
                name: "AssessmentID",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "CourseObjective",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "TechnicalRequirement",
                table: "Syllabuses");

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
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
                name: "TrainingFormat",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("51e5d305-4322-44f2-9989-dac1206924e7"), null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5636), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5645), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("6384f22b-9a49-456a-8716-a63d500086b7"), null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5659), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5659), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("a6f1d28b-9f14-4aeb-a27b-b2784c943487"), null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5678), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5679), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingContents_TrainingUnits_UnitCode",
                table: "TrainingContents",
                column: "UnitCode",
                principalTable: "TrainingUnits",
                principalColumn: "UnitCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
