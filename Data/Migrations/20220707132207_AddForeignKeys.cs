using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RecruiterId",
                table: "Recruitment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TechId",
                table: "Candidate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RecruiterId",
                table: "Candidate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitmentRequirement_SkillId",
                table: "RecruitmentRequirement",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_RecruiterId",
                table: "Recruitment",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruiterId",
                table: "Candidate",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruitmentId",
                table: "Candidate",
                column: "RecruitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_TechId",
                table: "Candidate",
                column: "TechId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Recruitment_RecruitmentId",
                table: "Candidate",
                column: "RecruitmentId",
                principalTable: "Recruitment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_RecruiterId",
                table: "Candidate",
                column: "RecruiterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate",
                column: "TechId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_User_RecruiterId",
                table: "Recruitment",
                column: "RecruiterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentRequirement_Recruitment_RecruitmentId",
                table: "RecruitmentRequirement",
                column: "RecruitmentId",
                principalTable: "Recruitment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentRequirement_Skill_SkillId",
                table: "RecruitmentRequirement",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Recruitment_RecruitmentId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_RecruiterId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_User_TechId",
                table: "Candidate");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_User_RecruiterId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentRequirement_Recruitment_RecruitmentId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentRequirement_Skill_SkillId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropIndex(
                name: "IX_RecruitmentRequirement_SkillId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_RecruiterId",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_RecruiterId",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_RecruitmentId",
                table: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Candidate_TechId",
                table: "Candidate");

            migrationBuilder.AlterColumn<string>(
                name: "RecruiterId",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TechId",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RecruiterId",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
