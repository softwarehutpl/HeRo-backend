using Data.Entities;

namespace Data.Repositories
{
    public class RecruitmentSkillRepository : BaseRepository<RecruitmentSkill>
    {
        private readonly DataContext context;
        public RecruitmentSkillRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        public RecruitmentSkill Get(int recruitmentId, int skillId)
        {
            RecruitmentSkill result = context.RecruitmentSkills.Find(recruitmentId, skillId);

            return result;
        }

        public IQueryable<RecruitmentSkill> GetAllRecruitmentSkills(int recruitmentId)
        {
            IQueryable<RecruitmentSkill> result = GetAll();
            result = result.Where(e => e.RecruitmentId == recruitmentId);

            return result;
        }

        public void Add(RecruitmentSkill recruitmentSkill)
        {
            AddAndSaveChanges(recruitmentSkill);
        }

        public void RemoveCampainRequrement(int id) { }
    }
}
