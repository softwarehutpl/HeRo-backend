using Common.ServiceRegistrationAttributes;
using Data.DTOs.Skill;
using Data.Entities;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface ISkillService
    {
        int AddSkill(string skillName);
        int DeleteSkill(int id);
        Skill GetSkill(int id);
        IEnumerable<Skill> GetSkills();
        IEnumerable<Skill> GetSkillsFilteredByName(string name);
        int UpdateSkill(UpdateSkillDTO dto);
    }
}