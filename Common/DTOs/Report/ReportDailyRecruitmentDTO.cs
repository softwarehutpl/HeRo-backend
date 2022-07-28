namespace Data.DTOs.Report
{
    public class ReportDailyRecruitmentDTO
    {
        public DateTime Date { get; set; }
        public int RecruitmentId { get; set; }
        public string RecruitmentName { get; set; }
        public int NumberOfCandidate { get; set; }
    }
}