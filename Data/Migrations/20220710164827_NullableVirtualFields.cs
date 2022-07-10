using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class NullableVirtualFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_DeletedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_LastUpdatedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_LastUpdatedById",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedById",
                table: "Recruitment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "TechId",
                table: "Candidate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedById",
                table: "Candidate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Candidate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_DeletedById",
                table: "Candidate",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_LastUpdatedById",
                table: "Candidate",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate",
                column: "TechId",
                principalTable: "User",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_LastUpdatedById",
                table: "Recruitment",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_DeletedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_LastUpdatedById",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_DeletedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_EndedById",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_LastUpdatedById",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedById",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "TechId",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastUpdatedById",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeletedById",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_DeletedById",
                table: "Candidate",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_LastUpdatedById",
                table: "Candidate",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate",
                column: "TechId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_LastUpdatedById",
                table: "Recruitment",
                column: "LastUpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
