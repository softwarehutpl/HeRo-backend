using Common.Listing;
using Data.DTO;
using Services.DTOs.Recruitment;

namespace Services.Listing
{
    public class RecruitmentListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<ReadRecruitmentDTO> ReadRecruitmentDTOs { set; get; }
        public RecruitmentFiltringDTO RecruitmentFiltringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }
    }
}