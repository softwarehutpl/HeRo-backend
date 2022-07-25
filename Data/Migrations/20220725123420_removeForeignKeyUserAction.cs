using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class removeForeignKeyUserAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAction_User_UserId",
                table: "UserAction");

            migrationBuilder.DropIndex(
                name: "IX_UserAction_UserId",
                table: "UserAction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserAction_UserId",
                table: "UserAction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAction_User_UserId",
                table: "UserAction",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
