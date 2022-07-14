using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CandidateModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_CreatedById",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_CreatedById",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RecruitmentRequirement");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Candidate");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Candidate",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableFrom",
                table: "Candidate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpectedMonthlySalary",
                table: "Candidate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HROpinionScore",
                table: "Candidate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HROpinionText",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewOpinionScore",
                table: "Candidate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterviewOpinionText",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherExpectations",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stage",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableFrom",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "ExpectedMonthlySalary",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "HROpinionScore",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "HROpinionText",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "InterviewOpinionScore",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "InterviewOpinionText",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "OtherExpectations",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Candidate");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Candidate",
                newName: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RecruitmentRequirement",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Candidate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CreatedById",
                table: "Candidate",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_CreatedById",
                table: "Candidate",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
