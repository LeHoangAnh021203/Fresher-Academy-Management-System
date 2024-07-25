using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixDBV120032024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",
                table: "LearningObjectives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_LearningObjectives_TrainingContents_Name",  
                table: "LearningObjectives", 
                column: "Name",
                principalTable: "TrainingContents",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
