using Common.Listing;

namespace HeRoBackEnd.ViewModels.Interview
{
    public class InterviewFiltringViewModel
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? CandidateId { get; set; }

        public int? WorkerId { get; set; }

        public string? Type { get; set; }

        public Paging Paging { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}