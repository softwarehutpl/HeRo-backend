using Data.Entities;

namespace Data.Repositories
{
    public class SkillRepository : BaseRepository<Skill>
    {
        public SkillRepository(DataContext context) : base(context)
        {

        }
        public bool Exists(string name)
        {
            IQueryable<Skill> skills = GetAll();
            bool result = skills.
                Any(x => x.Name == name);

            return result;
        }
    }

}
