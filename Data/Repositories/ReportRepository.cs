using Common.ServiceRegistrationAttributes;
using Data.DTOs.Report;
using Data.Entities.Report;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class ReportRepository : BaseRepository<PopularRecruitment>
    {
        public ReportRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<PopularRecruitment> GetPopularRecruitments()
        {
            var result = DataContext.PopularRecruitments.FromSqlInterpolated($"sp_GetPopularRecruitments");

            return result;
        }

        public IQueryable<RequestedSkill> GetRequestedSkills()
        {
            DateTime now = DateTime.UtcNow;
            var result = DataContext.RequestedSkills.FromSqlInterpolated($"sp_RequestedSkills {now}");

            return result;
        }

        public IQueryable<DailyRecruitment> CountNewCandidates(ReportCountDTO reportDTO)
        {
            DateTime now = DateTime.UtcNow;
            string ids = String.Join(",", reportDTO.Ids.ToArray());
            var result = DataContext
                .NewCandidates
                .FromSqlInterpolated($"sp_CountNewCandidates {ids}, {now}, {reportDTO.FromDate}, {reportDTO.ToDate}");

            return result;
        }
    }
}