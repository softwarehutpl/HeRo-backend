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
@now datetime
AS
SELECT TOP 5 recru.Id as RecruitmentId, MIN(recru.Name) as RecruitmentName, COUNT(recru.Id) as NumberOfCandidate
FROM (SELECT *
	  FROM Recruitment
	  WHERE Recruitment.EndingDate > @now
	  AND Recruitment.EndedDate is null
      AND Recruitment.DeletedById is null) as recru, Candidate
WHERE recru.Id = Candidate.RecruitmentId
GROUP BY recru.Id
ORDER BY COUNT(recru.Id) DESC
GO";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC sp_GetPopularRecruitments");
        }
    }
}