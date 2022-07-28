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
SELECT TOP 5 RecruitmentSkill.SkillId as SkillId, MIN(Skill.Name) as SkillName, COUNT(*) as Quantity
FROM (SELECT *
	    FROM Recruitment
	    WHERE Recruitment.EndingDate > @now
	    AND Recruitment.EndedDate is null
        AND Recruitment.DeletedById is null) as recru,
RecruitmentSkill, Skill
WHERE recru.Id = RecruitmentSkill.RecruitmentId
AND RecruitmentSkill.SkillId = Skill.Id
GROUP BY RecruitmentSkill.SkillId
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