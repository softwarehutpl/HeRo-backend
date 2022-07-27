using Microsoft.EntityFrameworkCore;

namespace Data.Entities.Report
{
    [Keyless]
    public class PopularRecruitment
    {
        public int RecruitmentId { get; set; }
        public int CandidateCount { get; set; }
    }
}