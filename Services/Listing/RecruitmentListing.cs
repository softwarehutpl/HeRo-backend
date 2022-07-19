using Common.Listing;
using Data.DTOs.Recruitment;

namespace Services.Listing
{
    public class RecruitmentListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<RecruitmentDTO> RecruitmentDTOs { set; get; }
        public RecruitmentFiltringDTO RecruitmentFiltringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }
    }
}