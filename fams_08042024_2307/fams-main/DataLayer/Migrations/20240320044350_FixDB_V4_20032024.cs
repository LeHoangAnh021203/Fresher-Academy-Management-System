using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixDBV420032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
             name: "IX_TrainingContents_Code",
             table: "TrainingContents",
             column: "Code");

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
            name: "FK_TrainingContents_LearningObjectives_Code",
            table: "TrainingContents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingContents_Code",
                table: "TrainingContents");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "TrainingContents");

        }
    }
}
