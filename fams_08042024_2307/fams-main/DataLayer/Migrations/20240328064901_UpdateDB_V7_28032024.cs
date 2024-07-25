using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV728032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5b93a925-bfd9-4468-b956-51535e8ce2ee"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("7f808b83-dd9c-4cdb-ae72-6f88e678012f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c2109c75-546f-45d9-9ae3-eb7cd41ff86e"));

            migrationBuilder.AlterColumn<string>(
                name: "ContentId",
                table: "TrainingContents",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("36c387c4-0151-4297-9ec0-eafc1a65918e"), null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7563), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7563), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("71d74177-10dd-4840-ab7f-e9f8c660747e"), null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7536), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7546), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("dd89dfad-4029-4374-a687-b15720fa7825"), null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7574), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7575), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("36c387c4-0151-4297-9ec0-eafc1a65918e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("71d74177-10dd-4840-ab7f-e9f8c660747e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("dd89dfad-4029-4374-a687-b15720fa7825"));

            migrationBuilder.AlterColumn<string>(
                name: "ContentId",
                table: "TrainingContents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("5b93a925-bfd9-4468-b956-51535e8ce2ee"), null, new DateTime(2024, 3, 20, 11, 49, 14, 249, DateTimeKind.Local).AddTicks(8093), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 49, 14, 249, DateTimeKind.Local).AddTicks(8102), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("7f808b83-dd9c-4cdb-ae72-6f88e678012f"), null, new DateTime(2024, 3, 20, 11, 49, 14, 249, DateTimeKind.Local).AddTicks(8156), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 20, 11, 49, 14, 249, DateTimeKind.Local).AddTicks(8157), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("c2109c75-546f-45d9-9ae3-eb7cd41ff86e"), null, new DateTime(2024, 3, 20, 11, 49, 14, 249, DateTimeKind.Local).AddTicks(8146), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 20, 11, 49, 14, 249, DateTimeKind.Local).AddTicks(8146), "Admin", "123@", 2, "0955422567", 0 }
                });
        }
    }
}
