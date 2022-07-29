namespace Data.DTOs.Report
{
    public class ReportDailyRecruitmentsDTO
    {
        public DateTime Date { get; set; }
        public List<ReportRecruitmentDTO> raportPopularRecruitmentDTOs { get; set; }
    }
}