namespace Data.DTOs.Report
{
    public class ReportDailyNewCandidatesDTO
    {
        public DateTime Date { get; set; }
        public IEnumerable<RaportRecruitmentDTO> raportPopularRecruitmentDTOs { get; set; }
    }
}