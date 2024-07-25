using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBV126022024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Code",
                table: "LearningObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllabusObjectives_LearningObjectives_LearningObjectiveCode",
                table: "SyllabusObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllabusObjectives_Syllabuses_SyllabusTopicCode",
                table: "SyllabusObjectives");

            migrationBuilder.DropIndex(
                name: "IX_SyllabusObjectives_LearningObjectiveCode",
                table: "SyllabusObjectives");

            migrationBuilder.DropIndex(
                name: "IX_SyllabusObjectives_SyllabusTopicCode",
                table: "SyllabusObjectives");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("0a7ddb12-ff95-4c85-bbe4-f1b583f2bf52"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("209bac26-fbc3-486f-9f7f-96cf4246990c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("533b3c28-f302-4c86-a252-1ed7e427c218"));

            migrationBuilder.DropColumn(
                name: "LearningObjectiveCode",
                table: "SyllabusObjectives");

            migrationBuilder.DropColumn(
                name: "SyllabusTopicCode",
                table: "SyllabusObjectives");

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "TrainingUnits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyllabusObjectives",
                table: "SyllabusObjectives",
                columns: new[] { "TopicCode", "ObjectiveCode" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("c17db5e2-fed2-45f6-ad07-26f1cf26a396"), null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3065), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3065), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("daca6e4a-cb9d-4a65-8bed-2cb5fa53a26e"), null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3000), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3008), "Super Admin", "123@", 1, "0987788762", 0 },
                    { new Guid("df251c2b-7ddc-42b9-8025-193aa860c899"), null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3075), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 26, 9, 33, 41, 535, DateTimeKind.Local).AddTicks(3076), "Trainer1", "123@", 3, "0876588765", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingContents_Code",
                table: "TrainingContents",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusObjectives_ObjectiveCode",
                table: "SyllabusObjectives",
                column: "ObjectiveCode");

            migrationBuilder.AddForeignKey(
                name: "FK_SyllabusObjectives_LearningObjectives_ObjectiveCode",
                table: "SyllabusObjectives",
                column: "ObjectiveCode",
                principalTable: "LearningObjectives",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyllabusObjectives_Syllabuses_TopicCode",
                table: "SyllabusObjectives",
                column: "TopicCode",
                principalTable: "Syllabuses",
                principalColumn: "TopicCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingContents_LearningObjectives_Code",
                table: "TrainingContents",
                column: "Code",
                principalTable: "LearningObjectives",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyllabusObjectives_LearningObjectives_ObjectiveCode",
                table: "SyllabusObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllabusObjectives_Syllabuses_TopicCode",
                table: "SyllabusObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingContents_LearningObjectives_Code",
                table: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingContents_Code",
                table: "TrainingContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SyllabusObjectives",
                table: "SyllabusObjectives");

            migrationBuilder.DropIndex(
                name: "IX_SyllabusObjectives_ObjectiveCode",
                table: "SyllabusObjectives");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("c17db5e2-fed2-45f6-ad07-26f1cf26a396"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("daca6e4a-cb9d-4a65-8bed-2cb5fa53a26e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("df251c2b-7ddc-42b9-8025-193aa860c899"));

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "TrainingUnits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "LearningObjectiveCode",
                table: "SyllabusObjectives",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SyllabusTopicCode",
                table: "SyllabusObjectives",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreateBy", "CreateDate", "DOB", "Email", "Gender", "ModifiedBy", "ModifiedTime", "Name", "Password", "PermissionId", "Phone", "Status" },
                values: new object[,]
                {
                    { new Guid("0a7ddb12-ff95-4c85-bbe4-f1b583f2bf52"), null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9249), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", 2, null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9250), "Admin", "123@", 2, "0955422567", 0 },
                    { new Guid("209bac26-fbc3-486f-9f7f-96cf4246990c"), null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9259), new DateTime(2003, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "trainer1@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9259), "Trainer1", "123@", 3, "0876588765", 0 },
                    { new Guid("533b3c28-f302-4c86-a252-1ed7e427c218"), null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9226), new DateTime(2000, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@gmail.com", 1, null, new DateTime(2024, 2, 21, 10, 50, 40, 442, DateTimeKind.Local).AddTicks(9236), "Super Admin", "123@", 1, "0987788762", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusObjectives_LearningObjectiveCode",
                table: "SyllabusObjectives",
                column: "LearningObjectiveCode");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusObjectives_SyllabusTopicCode",
                table: "SyllabusObjectives",
                column: "SyllabusTopicCode");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Code",
                table: "LearningObjectives",
                column: "Code",
                principalTable: "TrainingContents",
                principalColumn: "Content",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyllabusObjectives_LearningObjectives_LearningObjectiveCode",
                table: "SyllabusObjectives",
                column: "LearningObjectiveCode",
                principalTable: "LearningObjectives",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_SyllabusObjectives_Syllabuses_SyllabusTopicCode",
                table: "SyllabusObjectives",
                column: "SyllabusTopicCode",
                principalTable: "Syllabuses",
                principalColumn: "TopicCode");
        }
    }
}
