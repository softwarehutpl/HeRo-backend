namespace Data.DTOs.Recruitment
{
    public class RecruitmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string RecruitmentPosition { get; set; }
        public string Localization { get; set; }
        public string Seniority { get; set; }
        public int? CandidateCount { get; set; }
        public int? HiredCount { get; set; }
    }
}