namespace Services.DTOs.Interview
{
    public class InterviewDTO
    {
        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public int UserId { get; set; }

        public string Type { get; set; }

        public InterviewDTO(DateTime date, int condidateId, int userId, string type)
        {
            Date = date;
            CandidateId = condidateId;
            UserId = userId;
            Type = type;
        }
    }
}