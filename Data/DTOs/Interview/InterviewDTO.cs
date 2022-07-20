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

        public override bool Equals(object? obj)
        {
            return obj is InterviewDTO dTO &&
                   InterviewId == dTO.InterviewId &&
                   Date == dTO.Date &&
                   CandidateId == dTO.CandidateId &&
                   CandidateName == dTO.CandidateName &&
                   CandidateLastName == dTO.CandidateLastName &&
                   CandidateEmail == dTO.CandidateEmail &&
                   WorkerId == dTO.WorkerId &&
                   WorkerEmail == dTO.WorkerEmail &&
                   Type == dTO.Type;
        }
    }
}