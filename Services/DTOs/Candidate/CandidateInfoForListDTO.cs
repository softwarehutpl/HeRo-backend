namespace Services.DTOs.Candidate
{
    public class CandidateInfoForListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string RecruitmentName { get; set; }
        public string Status { get; set; }
        public string Stage { get; set; }
        public int? TechId { get; set; }
        public string TechAssignee { get; set; }
        public int? RecruiterId { get; set; }
        public string RecruiterAssignee { get; set; }

        public CandidateInfoForListDTO(int id, string name, string source, string recruitmentName, string status, string stage, int? techId, string techAssignee, int? recruiterId, string recruiterAssignee)
        {
            Id = id;
            Name = name;
            Source = source;
            RecruitmentName = recruitmentName;
            Status = status;
            Stage = stage;
            TechId = techId;
            TechAssignee = techAssignee;
            RecruiterId = recruiterId;
            RecruiterAssignee = recruiterAssignee;
        }
    }
}