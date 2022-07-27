using Microsoft.EntityFrameworkCore;

namespace Data.Entities.Report
{
    [Keyless]
    public class DailyRecruitment
    {
        public DateTime Date { get; set; }
        public int RecruitmentId { get; set; }
        public string RecruitmentName { get; set; }
        public int NumberOfCandidate { get; set; }
    }
}