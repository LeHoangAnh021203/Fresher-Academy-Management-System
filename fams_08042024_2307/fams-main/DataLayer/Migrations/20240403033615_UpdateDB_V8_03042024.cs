using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV803042024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("a1ec1999-1dc3-4c7e-bb98-0b9634f2e26f"), null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5219), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5219), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("b7447007-0d93-4d9d-aa39-651606e0fbac"), null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5230), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5230), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("f087053d-f29f-44d3-ba48-17e2b31f97e9"), null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5186), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5195), "Super Admin", "123@", 1, "0987788762", 0 }
                });

            migrationBuilder.AlterColumn<string>(
              name: "TokenId",
              table: "RefreshTokens",
              maxLength: 100,
              nullable: false,
              oldMaxLength: 100,
              oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenString",
                table: "RefreshTokens",
                nullable: false,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireAt",
                table: "RefreshTokens",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<int>(
                name: "Statuses",
                table: "RefreshTokens",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a1ec1999-1dc3-4c7e-bb98-0b9634f2e26f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b7447007-0d93-4d9d-aa39-651606e0fbac"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("f087053d-f29f-44d3-ba48-17e2b31f97e9"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("36c387c4-0151-4297-9ec0-eafc1a65918e"), null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7563), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7563), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("71d74177-10dd-4840-ab7f-e9f8c660747e"), null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7536), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7546), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("dd89dfad-4029-4374-a687-b15720fa7825"), null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7574), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 28, 13, 49, 0, 959, DateTimeKind.Local).AddTicks(7575), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.AlterColumn<string>(
                name: "TokenId",
                table: "RefreshTokens",
                maxLength: 100,
                nullable: true,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenString",
                table: "RefreshTokens",
                nullable: true,
                oldNullable: true);
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireAt",
                table: "RefreshTokens",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<int>(
                name: "Statuses",
                table: "RefreshTokens",
                nullable: false,
                defaultValue: 0);
        }
    }
}
