using Common.Listing;
using Services.DTOs.Interview;

namespace Services.Listing
{
    public class InterviewListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<InterviewDTO> InterviewDTOs { set; get; }
        public InterviewFiltringDTO InterviewFiltringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }
    }
}