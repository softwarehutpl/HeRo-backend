﻿using Common.Listing;
using Data.DTOs.Recruitment;

namespace Services.Listing
{
    public class RecruitmentListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<RecruitmentDetailsDTO> ReadRecruitmentDTOs { set; get; }
        public RecruitmentFiltringDTO RecruitmentFiltringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }
    }
}