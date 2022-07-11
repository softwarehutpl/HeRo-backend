using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangeDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
               name: "CreatedById",
               table: "Candidate",
               type: "int",
               nullable: false,
               defaultValue: null);

            migrationBuilder.AlterColumn<DateTime>(
               name: "DeletedById",
               table: "Candidate",
               type: "int",
               nullable: true,
               defaultValue: null);

            migrationBuilder.AlterColumn<DateTime>(
               name: "LastUpdatedById",
               table: "Candidate",
               type: "int",
               nullable: true,
               defaultValue: null);

            migrationBuilder.AlterColumn<DateTime>(
              name: "CreatedById",
              table: "Recruitment",
              type: "int",
              nullable: false,
              defaultValue: null);

            migrationBuilder.AlterColumn<DateTime>(
               name: "DeletedById",
               table: "Recruitment",
               type: "int",
               nullable: true,
               defaultValue: null);

            migrationBuilder.AlterColumn<DateTime>(
               name: "EndedById",
               table: "Recruitment",
               type: "int",
               nullable: true,
               defaultValue: null);

            migrationBuilder.AlterColumn<DateTime>(
               name: "LastUpdatedById",
               table: "Recruitment",
               type: "int",
               nullable: true,
               defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
               name: "CreatedById",
               table: "Candidate",
               type: "int",
               nullable: false,
               defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
               name: "DeletedById",
               table: "Candidate",
               type: "int",
               nullable: true,
               defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
               name: "LastUpdatedById",
               table: "Candidate",
               type: "int",
               nullable: true,
               defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
             name: "CreatedById",
             table: "Recruitment",
             type: "int",
             nullable: false,
             defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
               name: "DeletedById",
               table: "Recruitment",
               type: "int",
               nullable: true,
               defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
               name: "EndedById",
               table: "Recruitment",
               type: "int",
               nullable: true,
               defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
               name: "LastUpdatedById",
               table: "Recruitment",
               type: "int",
               nullable: true,
               defaultValue: 0);
        }
    }
}
