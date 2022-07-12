namespace Services.DTOs.Interview
{
    public class InterviewListingDTO
    {
        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public int UserId { get; set; }

        public string Type { get; set; }

        public InterviewListingDTO(DateTime date, int candidateId, int userId, string type)
        {
            Date = date;
            CandidateId = candidateId;
            UserId = userId;
            Type = type;
        }
    }
}
