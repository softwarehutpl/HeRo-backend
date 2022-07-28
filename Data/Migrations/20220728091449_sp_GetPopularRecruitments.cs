using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class sp_GetPopularRecruitments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP PROCEDURE IF EXISTS dbo.sp_GetPopularRecruitments
GO
CREATE PROCEDURE dbo.sp_GetPopularRecruitments
AS
SELECT TOP 5 Recruitment.Id as RecruitmentId, MIN(Recruitment.Name) as RecruitmentName, COUNT(Recruitment.Id) as NumberOfCandidate
FROM Recruitment, Candidate
WHERE Recruitment.Id = Candidate.RecruitmentId
GROUP BY Recruitment.Id
ORDER BY COUNT(Recruitment.Id) DESC
GO";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC sp_GetPopularRecruitments");
        }
    }
}