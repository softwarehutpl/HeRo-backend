namespace Data.DTOs.Recruitment
{
    public class ReadRecruitmentDTO
    {
        public int Id { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecruiterId { get; set; }
        public int? CandidateCount { get; set; }
        public string RecruitmentPosition { get; set; }
        public string Localization { get; set; }
        public string Seniority { get; set; }
        public int? HiredCount { get; set; }
    }
}