using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Imap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Port",
                table: "SmtpServers",
                newName: "SmptPort");

            migrationBuilder.AddColumn<string>(
                name: "Imap",
                table: "SmtpServers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ImapPort",
                table: "SmtpServers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imap",
                table: "SmtpServers");

            migrationBuilder.DropColumn(
                name: "ImapPort",
                table: "SmtpServers");

            migrationBuilder.RenameColumn(
                name: "SmptPort",
                table: "SmtpServers",
                newName: "Port");
        }
    }
}
