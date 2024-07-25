using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV903042024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "TrainingCalendar",
                columns: table => new
                {
                    CalendarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCalendar", x => x.CalendarId);
                    table.ForeignKey(
                        name: "FK_TrainingCalendar_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("68d986b6-40a7-4840-8a84-4e605df56b3e"), null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9863), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9864), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("874d0d28-f8ef-4030-8d9f-ed00464513ab"), null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9822), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9834), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("be216f85-08d3-4cf6-a652-cee493adba73"), null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9852), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9852), "Admin", "123@", 2, "0955422567", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCalendar_ClassId",
                table: "TrainingCalendar",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingCalendar");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("68d986b6-40a7-4840-8a84-4e605df56b3e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("874d0d28-f8ef-4030-8d9f-ed00464513ab"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("be216f85-08d3-4cf6-a652-cee493adba73"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("a1ec1999-1dc3-4c7e-bb98-0b9634f2e26f"), null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5219), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5219), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("b7447007-0d93-4d9d-aa39-651606e0fbac"), null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5230), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5230), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("f087053d-f29f-44d3-ba48-17e2b31f97e9"), null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5186), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 3, 10, 36, 15, 384, DateTimeKind.Local).AddTicks(5195), "Super Admin", "123@", 1, "0987788762", 0 }
                });
        }
    }
}
