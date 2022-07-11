using Common.Listing;

namespace HeRoBackEnd.ViewModels.Recruitment
{
    // do listy kandydatów
    public class CandidateListFilterViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int RecruitmentId { get; set; }
        
        public string Status { get; set; }
        public int RecruiterId { get; set; }
        public int? TechId { get; set; }
        public Paging Paging { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
