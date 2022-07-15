using AutoMapper;
using Common.Enums;
using Common.ServiceRegistrationAttributes;
using Data.DTO;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Data.DTOs.Recruitment;
using System.Security.Claims;
using Data.DTOs.RecruitmentSkill;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        private readonly IMapper _mapper;
        public RecruitmentRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;

        }

        public int GetRecruiterId(int id)
        {
            Recruitment recruitment = GetById(id);

            int result = recruitment.RecruiterId;

            return result;
        }
        public int GetRecruiterId(int id)

        public RecruitmentDetailsDTO? GetRecruitmentDTOById(int id)
        {
            RecruitmentDetailsDTO? result = DataContext.Recruitments
                .Include(x => x.Candidates)
                .Where(x => x.Id == id)
                .Where(e => !e.DeletedDate.HasValue)
                .Select(x => new RecruitmentDetailsDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    BeginningDate = x.BeginningDate,
                    EndingDate = x.EndingDate,
                    Description = x.Description,
                    Localization = x.Localization,
                    RecruiterId = x.RecruiterId,
                    RecruitmentPosition = x.RecruitmentPosition,
                    Seniority = x.Seniority,
                    CandidateCount = x.Candidates.Count(),
                    HiredCount = x.Candidates.Count(e => e.Status == CandidateStatuses.HIRED.ToString()),
                    Skills = x.Skills.Select(s => new RecruitmentSkillDetailsDTO()
                    {
                        SkillId = s.SkillId,
                        SkillLevel = s.SkillLevel,
                        Name = s.Skill.Name
                    }).ToList()
                })
                .FirstOrDefault();

            if (result == default) return null;

            return result;
        }
        public Recruitment GetById(int id)
        {
            Recruitment result = DataContext.Recruitments
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<RecruitmentDetailsDTO> GetAllRecruitmentsDTOs()
        {
            IEnumerable<RecruitmentDetailsDTO> result = DataContext.Recruitments
            .Include(x => x.Candidates)
            .Where(e => !e.DeletedDate.HasValue)
            .Select(x => new ReadRecruitmentDTO
            {
                Id = x.Id,
                Name = x.Name,
                BeginningDate = x.BeginningDate,
                EndingDate = x.EndingDate,
                Description = x.Description,
                Localization = x.Localization,
                RecruiterId = x.RecruiterId,
                RecruitmentPosition = x.RecruitmentPosition,
                Seniority = x.Seniority,
                CandidateCount = x.Candidates.Count(),
                HiredCount = x.Candidates.Count(e => e.Status == CandidateStatuses.HIRED.ToString())
            }).ToList();
        
            

            return result;
        }
}
}