namespace Services.DTOs.Interview
{
    public class InterviewCreateDTO
    {
        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public int WorkerId { get; set; }

        public string Type { get; set; }

        public InterviewCreateDTO(DateTime date, int candidateId, int workerId, string type)
        {
            Date = date;
            CandidateId = candidateId;
            WorkerId = workerId;
            Type = type;
        }
    }
}