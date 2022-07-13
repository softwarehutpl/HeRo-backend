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
        public RecruitmentSkill Get(int recruitmentId, int skillId)
        {
            RecruitmentSkill result = _context.RecruitmentSkills.Find(recruitmentId, skillId);

            return result;
        }
        public bool IsSkillUsed(int skillId)
        {
            bool result=GetAll().Any(e => e.SkillId == skillId);

            return result;
        }
    }
}
