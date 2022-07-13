using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddRecruitmentSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentRequirement_Recruitment_RecruitmentId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentRequirement_Skill_SkillId",
                table: "RecruitmentRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecruitmentRequirement",
                table: "RecruitmentRequirement");

            migrationBuilder.RenameTable(
                name: "RecruitmentRequirement",
                newName: "RecruitmentSkill");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitmentRequirement_SkillId",
                table: "RecruitmentSkill",
                newName: "IX_RecruitmentSkill_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecruitmentSkill",
                table: "RecruitmentSkill",
                columns: new[] { "RecruitmentId", "SkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentSkill_Recruitment_RecruitmentId",
                table: "RecruitmentSkill",
                column: "RecruitmentId",
                principalTable: "Recruitment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecruitmentSkill_Skill_SkillId",
                table: "RecruitmentSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentSkill_Recruitment_RecruitmentId",
                table: "RecruitmentSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_RecruitmentSkill_Skill_SkillId",
                table: "RecruitmentSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecruitmentSkill",
                table: "RecruitmentSkill");

            migrationBuilder.RenameTable(
                name: "RecruitmentSkill",
                newName: "RecruitmentRequirement");

            migrationBuilder.RenameIndex(
                name: "IX_RecruitmentSkill_SkillId",
                table: "RecruitmentRequirement",
                newName: "IX_RecruitmentRequirement_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecruitmentRequirement",
                table: "RecruitmentRequirement",
                columns: new[] { "RecruitmentId", "SkillId" });

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
    }
}
