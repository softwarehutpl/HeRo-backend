using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SmtpNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ImapAccounts",
                newName: "Host");

            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "SmtpAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "SmtpAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "SmtpAccounts");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "SmtpAccounts");

            migrationBuilder.RenameColumn(
                name: "Host",
                table: "ImapAccounts",
                newName: "Name");
        }
    }
}
