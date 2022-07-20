namespace Data.DTOs.Interview
{
    public class InterviewFiltringDTO
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? CandidateId { get; set; }

        public int? WorkerId { get; set; }

        public string? Type { get; set; }
    }
}