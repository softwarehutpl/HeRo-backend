using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CRUDRecruitmentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Recruitment");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndedDate",
                table: "Recruitment");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
