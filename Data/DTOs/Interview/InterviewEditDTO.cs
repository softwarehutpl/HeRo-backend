namespace Services.DTOs.Interview
{
    public class InterviewEditDTO
    {
        public int InterviewId { get; set; }

        public DateTime Date { get; set; }

        public int WorkerId { get; set; }

        public string Type { get; set; }

        public InterviewEditDTO(int interviewId, DateTime date, int workerId, string type)
        {
            InterviewId = interviewId;
            Date = date;
            WorkerId = workerId;
            Type = type;
        }
    }
}