using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class SkillRepository : BaseRepository<Skill>
    {
        public SkillRepository(DataContext context) : base(context)
        {

        }
        public bool Exists(int id, string name)
        {
            IQueryable<Skill> skills = GetAll();
            bool result = skills.
                Any(x => x.Name == name && x.Id!=id);

            return result;
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
