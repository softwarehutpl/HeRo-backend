using Data.Entities;

namespace Data.Repositories
{
    public class RecruitmentSkillRepository : BaseRepository<RecruitmentSkill>
    {
        public RecruitmentSkillRepository(DataContext context) : base(context)
        {

        }
        public RecruitmentSkill Get(int recruitmentId, int skillId)
        {
            return null;
        }

        public void GetAllCampainRequrements(int ecruitmentId) { }

        public void AddCampainRequrement(int id) { }

        public void RemoveCampainRequrement(int id) { }
    }
}
