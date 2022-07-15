namespace Services.DTOs.Candidate
{
    public class CandidateFilteringDTO
    {
        public List<string> Status { get; set; }
        public List<string> Stages { get; set; }

        public CandidateFilteringDTO(List<string> status, List<string> stages)
        {
            Status = status;
            Stages = stages;
        }
    }
}