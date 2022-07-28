using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class sp_CountNewCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP PROCEDURE IF EXISTS dbo.sp_CountNewCandidates
GO
CREATE PROCEDURE dbo.sp_CountNewCandidates
@recruitmentsId varchar(max),
@now dateTime2,
@fromDate dateTime2,
@toDate dateTime2
AS
SELECT CAST(Candidate.ApplicationDate AS DATE) as Date, recru.Id as RecruitmentId, MIN(recru.Name) as RecruitmentName, COUNT(Candidate.Id) as NumberOfCandidate
FROM (SELECT *
	    FROM Recruitment
	    WHERE Recruitment.EndingDate > @now
	    AND Recruitment.EndedDate is null
        AND Recruitment.DeletedDate is null
	    AND Recruitment.Id IN (select value FROM string_split(@recruitmentsId,','))) as recru,
Candidate
WHERE Candidate.RecruitmentId = recru.Id
AND Candidate.ApplicationDate >= @fromDate
AND Candidate.ApplicationDate <= @toDate
GROUP BY CAST(Candidate.ApplicationDate AS DATE), recru.Id
ORDER BY CAST(Candidate.ApplicationDate AS DATE)
GO";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC sp_CountNewCandidates");
        }
    }
}