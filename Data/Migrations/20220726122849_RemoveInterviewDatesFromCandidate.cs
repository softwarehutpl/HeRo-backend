using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveInterviewDatesFromCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "TechInterviewDate",
                table: "Candidate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewDate",
                table: "Candidate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TechInterviewDate",
                table: "Candidate",
                type: "datetime2",
                nullable: true);
        }
    }
}
