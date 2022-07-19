namespace Data.DTOs.Recruitment
{
    public class RecruitmentFiltringDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool ShowOpen { get; set; }
        public bool ShowClosed { get; set; }
        public DateTime? BeginningDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public bool ShowPrivate { get; set; }

        public RecruitmentFiltringDTO(string? name, string? description, DateTime? beginningDate, DateTime? endingDate, bool showPrivate, bool showOpen, bool showClosed)
        {
            Name = name;
            Description = description;
            BeginningDate = beginningDate;
            EndingDate = endingDate;
            ShowPrivate = showPrivate;
            ShowOpen= showOpen;
            ShowClosed = showClosed;
        }
    }
}