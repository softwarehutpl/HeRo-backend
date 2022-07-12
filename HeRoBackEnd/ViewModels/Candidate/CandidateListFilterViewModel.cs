using Common.Listing;

namespace HeRoBackEnd.ViewModels.Candidate
{
    // do listy kandydatów
    public class CandidateListFilterViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Source { get; set; }
        //public int RecruitmentId { get; set; }
        
        public string Status { get; set; }
        public string Position { get; set; }
        public string Stage { get; set; }
        public int? TechId { get; set; }
        public int? RecruiterId { get; set; }
        public int RecruitmentId { get; set; }
        public Paging Paging { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
