using Common.ServiceRegistrationAttributes;
using Data.DTOs.RecruitmentSkill;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface IRecruitmentSkillRepository : IBaseRepository<RecruitmentSkill>
    {
        IEnumerable<RecruitmentSkillDTO> GetAllRecruitmentSkills(int recruitmentId);

        bool IsSkillUsed(int skillId);
    }
}