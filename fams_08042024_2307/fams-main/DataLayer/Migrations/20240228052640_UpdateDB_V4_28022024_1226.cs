using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV4280220241226 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentID",
                table: "Syllabuses",
                type: "nvarchar(5)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    AssessmentID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    QuizCount = table.Column<int>(type: "int", nullable: false),
                    QuizPercent = table.Column<int>(type: "int", nullable: false),
                    AssignmentCount = table.Column<int>(type: "int", nullable: false),
                    AssignmentPercent = table.Column<double>(type: "float", nullable: false),
                    FinalTheoryPercent = table.Column<double>(type: "float", nullable: false),
                    FinalPracticePercent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.AssessmentID);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("57c005a0-b5ca-4d9a-87e8-836595f028b6"), null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(58), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(59), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("af8acd56-2618-4fb3-8e0a-7bc8188e56bf"), null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(69), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(70), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("fd006e94-81e3-4c96-8198-9e1a52bba517"), null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(35), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 12, 26, 40, 565, DateTimeKind.Local).AddTicks(45), "Super Admin", "123@", 1, "0987788762", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_AssessmentID",
                table: "Syllabuses",
                column: "AssessmentID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Assessments_AssessmentID",
                table: "Syllabuses",
                column: "AssessmentID",
                principalTable: "Assessments",
                principalColumn: "AssessmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Assessments_AssessmentID",
                table: "Syllabuses");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_AssessmentID",
                table: "Syllabuses");

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

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentID",
                table: "Syllabuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("326962b0-d21b-4df2-96e3-64677295d4fd"), null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9822), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9831), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("ba1903b1-47a2-485a-8b36-a85b3e1557fd"), null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9844), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9845), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("df1efe50-04c0-45ff-ace0-2e525df6c0f8"), null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9856), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 11, 32, 20, 584, DateTimeKind.Local).AddTicks(9857), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }
    }
}
