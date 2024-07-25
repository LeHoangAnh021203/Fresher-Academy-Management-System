using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangePKTrainingContent030102024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryType",
                table: "TrainingContents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "ContentId",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("0996d074-d8d7-49cf-af0a-73e4e9e1aee2"), null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4275), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4275), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("2a638fb0-d2a1-4f82-ab34-a6baed2fa69a"), null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4253), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4265), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("52bee380-a77f-42b4-8651-5be375f9a056"), null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4281), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 1, 14, 49, 16, 805, DateTimeKind.Local).AddTicks(4281), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "TrainingContents");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryType",
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
                    { new Guid("507636d7-7061-41cf-b321-47086784ba4d"), null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3215), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3225), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("5b8d557a-5406-4d2d-adbd-58bfacbdab7b"), null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3247), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3248), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("fec5f33a-a8ed-4b33-b97a-b7b75f94fa63"), null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3238), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 28, 15, 55, 55, 595, DateTimeKind.Local).AddTicks(3238), "Admin", "123@", 2, "0955422567", 0 }
                });
        }
    }
}
