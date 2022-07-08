namespace Services.DTOs.Recruitment
{
    public class RecruitmentFiltringDTO
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime? BeginningDate { get; set; }
        public DateTime? EndingDate { get; set; }

        public RecruitmentFiltringDTO(string name, string status, string description, DateTime? beginningDate, DateTime? endingDate)
        {
            Name = name;
            Status = status;
            Description = description;
            BeginningDate = beginningDate;
            EndingDate = endingDate;    
        }
    }
}
