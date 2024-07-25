using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV1003042024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingCalendar_Classes_ClassId",
                table: "TrainingCalendar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingCalendar",
                table: "TrainingCalendar");

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

            migrationBuilder.RenameTable(
                name: "TrainingCalendar",
                newName: "TrainingCalendars");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingCalendar_ClassId",
                table: "TrainingCalendars",
                newName: "IX_TrainingCalendars_ClassId");

            migrationBuilder.AddColumn<int>(
                name: "Attendee",
                table: "TrainingCalendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingCalendars",
                table: "TrainingCalendars",
                column: "CalendarId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("5a5dc416-4214-44a0-aa30-35179a143e25"), null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9643), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9644), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("b9834803-6b36-49a5-bde1-2b664f30eb11"), null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9633), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9634), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("cda6f292-29a9-4e23-bf07-f71e01eea284"), null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9610), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 46, 17, 432, DateTimeKind.Local).AddTicks(9618), "Super Admin", "123@", 1, "0987788762", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingCalendars_Classes_ClassId",
                table: "TrainingCalendars",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingCalendars_Classes_ClassId",
                table: "TrainingCalendars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingCalendars",
                table: "TrainingCalendars");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5a5dc416-4214-44a0-aa30-35179a143e25"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b9834803-6b36-49a5-bde1-2b664f30eb11"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("cda6f292-29a9-4e23-bf07-f71e01eea284"));

            migrationBuilder.DropColumn(
                name: "Attendee",
                table: "TrainingCalendars");

            migrationBuilder.RenameTable(
                name: "TrainingCalendars",
                newName: "TrainingCalendar");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingCalendars_ClassId",
                table: "TrainingCalendar",
                newName: "IX_TrainingCalendar_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingCalendar",
                table: "TrainingCalendar",
                column: "CalendarId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("68d986b6-40a7-4840-8a84-4e605df56b3e"), null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9863), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9864), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("874d0d28-f8ef-4030-8d9f-ed00464513ab"), null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9822), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9834), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("be216f85-08d3-4cf6-a652-cee493adba73"), null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9852), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 4, 3, 13, 29, 59, 150, DateTimeKind.Local).AddTicks(9852), "Admin", "123@", 2, "0955422567", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingCalendar_Classes_ClassId",
                table: "TrainingCalendar",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
