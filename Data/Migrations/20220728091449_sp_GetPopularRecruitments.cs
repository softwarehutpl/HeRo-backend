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
SELECT TOP 5 Recruitment.Id as RecruitmentId, Recruitment.Name as RecruitmentName, COUNT(Recruitment.Id) as NumberOfCandidate
FROM Recruitment JOIN Candidate
ON Recruitment.Id = Candidate.RecruitmentId
WHERE Recruitment.EndingDate > @now
    AND Recruitment.EndedDate is null
    AND Recruitment.DeletedDate is null
GROUP BY Recruitment.Id, Recruitment.Name
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