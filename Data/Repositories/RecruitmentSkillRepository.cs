using AutoMapper;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.RecruitmentSkill;
using Data.Entities;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class RecruitmentSkillRepository : BaseRepository<RecruitmentSkill>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RecruitmentSkillRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool IsSkillUsed(int skillId)
        {
            bool result=GetAll()
                .Any(e => e.SkillId == skillId);

            return result;
        }
        public IEnumerable<RecruitmentSkillDTO> GetAllRecruitmentSkills(int recruitmentId)
        {
            IEnumerable<RecruitmentSkillDTO> result = GetAll()
                .Where(e => e.RecruitmentId == recruitmentId)
                .Select(e => _mapper.Map<RecruitmentSkillDTO>(e));

            return result;
        }
    }
}
