using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV3260220241225 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgarmSyllabuses_TrainingPrograms_TopicCode",
                table: "TrainingProgarmSyllabuses");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("2f42e333-2630-49b0-87da-650d3640c827"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("37296286-89f4-4433-806a-79c55e5bd3ec"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b614fe59-0d98-463e-9dc1-7258a9ba159e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("51e5d305-4322-44f2-9989-dac1206924e7"), null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5636), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5645), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("6384f22b-9a49-456a-8716-a63d500086b7"), null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5659), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5659), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("a6f1d28b-9f14-4aeb-a27b-b2784c943487"), null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5678), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 25, 6, 935, DateTimeKind.Local).AddTicks(5679), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingProgarmSyllabuses_TrainingProgramCode",
                table: "TrainingProgarmSyllabuses",
                column: "TrainingProgramCode");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgarmSyllabuses_TrainingPrograms_TrainingProgramCode",
                table: "TrainingProgarmSyllabuses",
                column: "TrainingProgramCode",
                principalTable: "TrainingPrograms",
                principalColumn: "TrainingProgramCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgarmSyllabuses_TrainingPrograms_TrainingProgramCode",
                table: "TrainingProgarmSyllabuses");

            migrationBuilder.DropIndex(
                name: "IX_TrainingProgarmSyllabuses_TrainingProgramCode",
                table: "TrainingProgarmSyllabuses");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("2f42e333-2630-49b0-87da-650d3640c827"), null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4235), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4244), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("37296286-89f4-4433-806a-79c55e5bd3ec"), null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4258), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4259), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("b614fe59-0d98-463e-9dc1-7258a9ba159e"), null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4277), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 26, 12, 22, 11, 385, DateTimeKind.Local).AddTicks(4278), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgarmSyllabuses_TrainingPrograms_TopicCode",
                table: "TrainingProgarmSyllabuses",
                column: "TopicCode",
                principalTable: "TrainingPrograms",
                principalColumn: "TrainingProgramCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
