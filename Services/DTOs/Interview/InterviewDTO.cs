namespace Services.DTOs.Interview
{
    public class InterviewDTO
    {
        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public int UserId { get; set; }

        public InterviewDTO(DateTime date, int condidateId, int userId)
        {
            Date = date;
            CandidateId = condidateId;
            UserId = userId;
        }
    }
}
