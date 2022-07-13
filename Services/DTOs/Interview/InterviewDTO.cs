namespace Services.DTOs.Interview
{
    public class InterviewDTO
    {
        public int InterviewId { get; set; }

        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public string CandidateEmail { get; set; }

        public int WorkerId { get; set; }

        public string WorkerEmail { get; set; }

        public string Type { get; set; }

        public InterviewDTO(int interviewId, DateTime date, int candidateId, string candidateEmail, int workerId, string workerEmail, string type)
        {
            InterviewId = interviewId;
            Date = date;
            CandidateId = candidateId;
            CandidateEmail = candidateEmail;
            WorkerId = workerId;
            WorkerEmail = workerEmail;
            Type = type;
        }
    }
}