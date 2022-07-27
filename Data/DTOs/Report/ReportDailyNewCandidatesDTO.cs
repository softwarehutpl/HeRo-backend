namespace Data.DTOs.Report
{
    public class ReportDailyNewCandidatesDTO
    {
        public DateTime Date { get; set; }
        public List<RaportRecruitmentDTO> raportPopularRecruitmentDTOs { get; set; }
    }
}