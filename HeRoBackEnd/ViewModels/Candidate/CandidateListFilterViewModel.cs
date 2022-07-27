using Common.Listing;

namespace HeRoBackEnd.ViewModels.Candidate
{
    public class CandidateListFilterViewModel
    {
        public int? RecruitmentId { get; set; }
        public List<string>? Status { get; set; }
        public List<string>? Stage { get; set; }
        public Paging Paging { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}