using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("14cde510-371a-44e2-975b-fdebb087d6f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9db22591-bf69-4b41-a067-d93bda263214"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a061d0fb-7fc1-4e48-be07-3d24a2f6c720"));

            migrationBuilder.AlterColumn<double>(
                name: "QuizPercent",
                table: "Assessments",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("507636d7-7061-41cf-b321-47086784ba4d"), null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3215), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3225), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("5b8d557a-5406-4d2d-adbd-58bfacbdab7b"), null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3247), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3248), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("fec5f33a-a8ed-4b33-b97a-b7b75f94fa63"), null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3238), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3238), "Admin", "123@", 2, "0955422567", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("507636d7-7061-41cf-b321-47086784ba4d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5b8d557a-5406-4d2d-adbd-58bfacbdab7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("fec5f33a-a8ed-4b33-b97a-b7b75f94fa63"));

            migrationBuilder.AlterColumn<int>(
                name: "QuizPercent",
                table: "Assessments",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("14cde510-371a-44e2-975b-fdebb087d6f0"), null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(309), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(310), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("9db22591-bf69-4b41-a067-d93bda263214"), null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(300), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(301), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("a061d0fb-7fc1-4e48-be07-3d24a2f6c720"), null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(275), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 13, 55, 28, 833, DateTimeKind.Local).AddTicks(284), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }
    }
}
