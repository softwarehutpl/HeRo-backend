namespace Services.DTOs.Interview
{
    public class InterviewEditDTO
    {
        public int InterviewId { get; set; }

        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public int UserId { get; set; }

        public string Type { get; set; }

        public InterviewEditDTO(int interviewId, DateTime date, int candidateId, int userId, string type)
        {
            InterviewId = interviewId;
            Date = date;
            CandidateId = candidateId;
            UserId = userId;
            Type = type;
        }
    }
}