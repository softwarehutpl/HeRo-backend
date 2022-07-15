using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddLocalizationSeniorityAndPositionFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecruitmentPosition",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Seniority",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localization",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "RecruitmentPosition",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Seniority",
                table: "Recruitment");
        }
    }
}
