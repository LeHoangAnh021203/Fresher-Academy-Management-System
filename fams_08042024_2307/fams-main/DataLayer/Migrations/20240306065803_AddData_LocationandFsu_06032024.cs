using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddDataLocationandFsu06032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("70f87b26-c665-47fa-9f2a-1d00793f26c5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9665d1b7-3fe5-460a-9cf5-14f8460b4f16"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("eab0568e-01dc-4598-92a7-85046223068e"));

            migrationBuilder.InsertData(
                table: "Fsus",
                columns: new[] { "FsuId", "FsuName" },
                values: new object[,]
                {
                    { "F001", "FHM" },
                    { "F002", "FDM" },
                    { "F003", "FSE" },
                    { "F004", "FWB" },
                    { "F005", "FWA" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "LocationName" },
                values: new object[,]
                {
                    { "L001", "FTown 1" },
                    { "L002", "FTown 2" },
                    { "L003", "FTown 3" },
                    { "L004", "FTown 4" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("326ffd0c-33a6-447f-9e87-0b111c97684e"), null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7385), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7385), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("8046fe60-98e4-44c5-81d2-d386c6eed693"), null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7353), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7361), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("e7aa6c31-d5f0-4c16-89bf-c301de749311"), null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7375), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 6, 13, 58, 3, 30, DateTimeKind.Local).AddTicks(7376), "Admin", "123@", 2, "0955422567", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fsus",
                keyColumn: "FsuId",
                keyValue: "F001");

            migrationBuilder.DeleteData(
                table: "Fsus",
                keyColumn: "FsuId",
                keyValue: "F002");

            migrationBuilder.DeleteData(
                table: "Fsus",
                keyColumn: "FsuId",
                keyValue: "F003");

            migrationBuilder.DeleteData(
                table: "Fsus",
                keyColumn: "FsuId",
                keyValue: "F004");

            migrationBuilder.DeleteData(
                table: "Fsus",
                keyColumn: "FsuId",
                keyValue: "F005");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: "L001");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: "L002");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: "L003");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: "L004");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("326ffd0c-33a6-447f-9e87-0b111c97684e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("8046fe60-98e4-44c5-81d2-d386c6eed693"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("e7aa6c31-d5f0-4c16-89bf-c301de749311"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("70f87b26-c665-47fa-9f2a-1d00793f26c5"), null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1389), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1397), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("9665d1b7-3fe5-460a-9cf5-14f8460b4f16"), null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1410), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1411), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("eab0568e-01dc-4598-92a7-85046223068e"), null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1419), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1420), "Trainer1", "123@", 3, "0876588765", 0 }
                });
        }
    }
}
