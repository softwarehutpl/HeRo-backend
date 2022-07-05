namespace Data.Repositories
{
    public interface ISkillRepository
    {
        void AddSkill(int id);
        void GetAllSkills();
        void GetSkillById(int id);
        void RemoveSkill(int id);
    }
}