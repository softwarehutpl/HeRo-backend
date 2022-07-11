using Data.Entities;

namespace Data.Repositories
{
    public class SkillRepository : BaseRepository<Skill>
    {
        public SkillRepository(DataContext context) : base(context)
        {

        }
        public Skill GetSkillById(int id)
        {
            Skill result = GetById(id);

            if (result == default) return null;

            return result;
        }

        public IQueryable<Skill> GetAllSkills()
        {
            IQueryable<Skill> result = GetAll();

            return result;
        }

        public void AddSkill(Skill skill)
        {
            AddAndSaveChanges(skill);
        }

        public void UpdateSkill(Skill skill)
        {
            UpdateAndSaveChanges(skill);
        }

        public void DeleteSkill(int id)
        {
            RemoveByIdAndSaveChanges(id);
        }

    }

}
