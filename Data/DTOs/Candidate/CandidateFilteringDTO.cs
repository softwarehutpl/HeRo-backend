namespace Data.DTOs.Candidate
{
    public class CandidateFilteringDTO
    {
        public int? RecruitmentId { get; set; }
        public List<string>? Status { get; set; }
        public List<string>? Stages { get; set; }
    }
}