using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class sp_RequestedSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP PROCEDURE IF EXISTS dbo.sp_RequestedSkills
GO
CREATE PROCEDURE dbo.sp_RequestedSkills
@now datetime
AS
SELECT TOP 5 RecruitmentSkill.SkillId as SkillId, Skill.Name as SkillName, COUNT(*) as Quantity
FROM Recruitment JOIN RecruitmentSkill
ON Recruitment.Id = RecruitmentSkill.RecruitmentId
JOIN Skill
ON RecruitmentSkill.SkillId = Skill.Id
WHERE Recruitment.EndingDate > @now
	AND Recruitment.EndedDate is null
    AND Recruitment.DeletedDate is null
GROUP BY RecruitmentSkill.SkillId, Skill.Name
ORDER BY COUNT(*) DESC
GO";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC sp_RequestedSkills");
        }
    }
}