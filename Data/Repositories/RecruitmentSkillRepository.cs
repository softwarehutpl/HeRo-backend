using Data.Entities;

namespace Data.Repositories
{
    public class RecruitmentSkillRepository : BaseRepository<RecruitmentSkill>
    {
        private readonly DataContext _context;
        public RecruitmentSkillRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public bool IsSkillUsed(int skillId)
        {
            bool result=GetAll().Any(e => e.SkillId == skillId);

            return result;
        }
    }
}
