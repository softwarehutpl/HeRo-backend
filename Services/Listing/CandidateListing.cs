using Common.Listing;
using Data.DTOs.Candidate;

namespace Services.Listing
{
    public class CandidateListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<CandidateInfoForListDTO> CandidateInfoForListDTOs { set; get; }
        public CandidateFilteringDTO CandidateFilteringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }
    }
}