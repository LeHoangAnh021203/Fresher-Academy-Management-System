using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBAddTablesLocationandFsu06032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Assessments_AssessmentID",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_AssessmentID",
                table: "Syllabuses");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("7e1d7921-9162-42c6-b6b5-b4ad3fdedc96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a9bde18c-f82c-452c-a0e8-5d862b2a1ec3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c3f677ad-638e-43bb-ac0a-0bcdb97ed0da"));

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "TrainingPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifyBy",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "TrainingPrograms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentID",
                table: "Syllabuses",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)");

            migrationBuilder.AlterColumn<string>(
                name: "FSU",
                table: "Classes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.CreateTable(
                name: "Fsus",
                columns: table => new
                {
                    FsuId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FsuName = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fsus", x => x.FsuId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("70f87b26-c665-47fa-9f2a-1d00793f26c5"), null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1389), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1397), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("9665d1b7-3fe5-460a-9cf5-14f8460b4f16"), null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1410), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1411), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("eab0568e-01dc-4598-92a7-85046223068e"), null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1419), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 6, 13, 56, 18, 177, DateTimeKind.Local).AddTicks(1420), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_AssessmentID",
                table: "Syllabuses",
                column: "AssessmentID",
                unique: true,
                filter: "[AssessmentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_FSU",
                table: "Classes",
                column: "FSU");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Location",
                table: "Classes",
                column: "Location");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Fsus_FSU",
                table: "Classes",
                column: "FSU",
                principalTable: "Fsus",
                principalColumn: "FsuId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Locations_Location",
                table: "Classes",
                column: "Location",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Assessments_AssessmentID",
                table: "Syllabuses",
                column: "AssessmentID",
                principalTable: "Assessments",
                principalColumn: "AssessmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Fsus_FSU",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Locations_Location",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Assessments_AssessmentID",
                table: "Syllabuses");

            migrationBuilder.DropTable(
                name: "Fsus");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_AssessmentID",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Classes_FSU",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_Location",
                table: "Classes");

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

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "TrainingPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentID",
                table: "Syllabuses",
                type: "nvarchar(5)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FSU",
                table: "Classes",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("7e1d7921-9162-42c6-b6b5-b4ad3fdedc96"), null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4130), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4130), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("a9bde18c-f82c-452c-a0e8-5d862b2a1ec3"), null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4107), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4115), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("c3f677ad-638e-43bb-ac0a-0bcdb97ed0da"), null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4124), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 3, 1, 15, 8, 30, 160, DateTimeKind.Local).AddTicks(4124), "Admin", "123@", 2, "0955422567", 0 }
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
    }
}
