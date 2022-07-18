namespace Data.DTOs.Interview
{
    public class InterviewDTO
    {
        public int InterviewId { get; set; }

        public DateTime Date { get; set; }

        public int CandidateId { get; set; }

        public string CandidateName { get; set; }

        public string CandidateLastName { get; set; }

        public string CandidateEmail { get; set; }

        public int WorkerId { get; set; }

        public string WorkerEmail { get; set; }

        public string Type { get; set; }

        public InterviewDTO(int interviewId, DateTime date, int candidateId, string candidateName, string candidateLastName, string candidateEmail, int workerId, string workerEmail, string type)
        {
            InterviewId = interviewId;
            Date = date;
            CandidateId = candidateId;
            CandidateName = candidateName;
            CandidateLastName = candidateLastName;
            CandidateEmail = candidateEmail;
            WorkerId = workerId;
            WorkerEmail = workerEmail;
            Type = type;
        }
    }
}