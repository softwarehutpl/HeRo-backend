namespace Services.DTOs.Interview
{
    public class InterviewFiltringDTO
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int? CandidateId { get; set; }

        public int? WorkerId { get; set; }

        public string? Type { get; set; }

        public InterviewFiltringDTO(DateTime fromDate, DateTime toDate, int? candidateId, int? workerId, string? type)
        {
            FromDate = fromDate;
            ToDate = toDate;
            CandidateId = candidateId;
            WorkerId = workerId;
            Type = type;
        }
    }
}