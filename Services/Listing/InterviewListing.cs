using Common.Listing;
using Data.DTOs.Interview;

namespace Services.Listing
{
    public class InterviewListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<InterviewDTO> InterviewDTOs { set; get; }
        public InterviewFiltringDTO InterviewFiltringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }

        public override bool Equals(object? obj)
        {
            return obj is InterviewListing listing &&
                   TotalCount == listing.TotalCount &&
                   EqualityComparer<IEnumerable<InterviewDTO>>.Default.Equals(InterviewDTOs, listing.InterviewDTOs) &&
                   EqualityComparer<InterviewFiltringDTO>.Default.Equals(InterviewFiltringDTO, listing.InterviewFiltringDTO) &&
                   EqualityComparer<SortOrder>.Default.Equals(SortOrder, listing.SortOrder) &&
                   EqualityComparer<Paging>.Default.Equals(Paging, listing.Paging);
        }
    }
}