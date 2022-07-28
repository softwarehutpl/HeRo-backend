using Common.ServiceRegistrationAttributes;
using Data.DTOs.Recruitment;
using Data.DTOs.Report;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface IRecruitmentRepository : IBaseRepository<Recruitment>
    {
        IEnumerable<RecruitmentDetailsDTO> GetAllRecruitmentsDTOs();

        int GetRecruiterId(int id);

        Recruitment GetRecruitmentDetails(int recruitmentId);

        RecruitmentDetailsDTO? GetRecruitmentDTOById(int id);
    }
}