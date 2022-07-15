using AutoMapper;
using Common.Enums;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Data.DTOs.Recruitment;
using System.Security.Claims;
using Data.DTOs.RecruitmentSkill;

namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        private readonly IMapper _mapper;
        private readonly RecruitmentSkillRepository _recruitmentSkillRepository;
        public RecruitmentRepository(DataContext context, IMapper mapper, RecruitmentSkillRepository repo) : base(context)
        {
            _mapper = mapper;
            _recruitmentSkillRepository = repo;
        }
        public RecruitmentDetailsDTO GetRecruitmentById(int id)
        {
            Recruitment recruitment = GetById(id);

            if(recruitment==null)
            {
                return null;
            }
            if (recruitment.DeletedDate.HasValue)
            {
                return null;
            }

            RecruitmentDetailsDTO result = _mapper.Map<RecruitmentDetailsDTO>(recruitment);
            result.Skills = _recruitmentSkillRepository.GetAllRecruitmentSkills(result.Id);

            return result;
        }

        public List<RecruitmentDetailsDTO> GetAllRecruitments()
        {
            List<RecruitmentDetailsDTO> result = GetAll()
                .Where(e => !(e.DeletedDate.HasValue))
                .Select(e => new RecruitmentDetailsDTO()
                {
                    Id = e.Id,
                    BeginningDate=e.BeginningDate,
                    EndingDate=e.EndingDate,
                    Name=e.Name,
                    Description=e.Description,
                    RecruiterId=e.RecruiterId,
                    Skills = _recruitmentSkillRepository.GetAllRecruitmentSkills(e.Id)
                })
                .ToList();               

            return result;
        }
        public int GetRecruiterId(int id)
        {
            var result = DataContext.Recruitments.
                Select(x => x.RecruiterId).
                FirstOrDefault();
            
            return result;
        }
        public Recruitment GetById(int id)
        {
            Recruitment result = base.GetById(id);

            if(result==null) return null;

            IEnumerable<RecruitmentSkillDTO> skillDtos = _recruitmentSkillRepository.GetAllRecruitmentSkills(id);
            IEnumerable<RecruitmentSkill> skills= _mapper.Map<IEnumerable<RecruitmentSkill>>(skillDtos);
            result.Skills = (ICollection<RecruitmentSkill>)skills;

            return result;
        }
    }
}
