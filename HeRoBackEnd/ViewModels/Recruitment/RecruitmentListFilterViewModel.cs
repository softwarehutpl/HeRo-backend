using Common.Listing;

namespace HeRoBackEnd.ViewModels.Recruitment
{
    public class RecruitmentListFilterViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool ShowOpen { get; set; }
        public bool ShowClosed { get; set; }
        public DateTime? BeginningDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public Paging Paging { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}