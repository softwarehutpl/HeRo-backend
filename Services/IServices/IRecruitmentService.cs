using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Recruitment;
using Services.Listing;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface IRecruitmentService
    {
        int AddRecruitment(CreateRecruitmentDTO dto);
        int DeleteRecruitment(DeleteRecruitmentDTO dto);
        int EndRecruitment(EndRecruimentDTO dto);
        RecruitmentDetailsDTO GetRecruitment(int recruitmentId);
        RecruitmentListing GetRecruitments(Paging paging, SortOrder? sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO);
        int UpdateRecruitment(int recruitmentId, UpdateRecruitmentDTO dto);
    }
}