using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RecruitmentCRUDFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "EndedById",
                table: "Recruitment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Recruitment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment",
                column: "EndedById",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "EndedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment",
                column: "EndedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
