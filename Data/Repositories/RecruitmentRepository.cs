using Common.Enums;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Recruitment;
using Data.DTOs.RecruitmentSkill;
using Data.Entities;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    [ScopedRegistrationWithInterface]
    public class RecruitmentRepository : BaseRepository<Recruitment>, IRecruitmentRepository
    {
        public RecruitmentRepository(DataContext context) : base(context)
        {
        }

        public int GetRecruiterId(int id)
        {
            Recruitment recruitment = GetById(id);

            if (recruitment == null)
            {
                return 0;
            }

            int result = recruitment.RecruiterId;

            return result;
        }

        public RecruitmentDetailsDTO? GetRecruitmentDTOById(int id)
        {
            RecruitmentDetailsDTO? result = DataContext.Recruitments
                //.Include(x => x.Candidates)
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

        public IEnumerable<RecruitmentDetailsDTO> GetAllRecruitmentsDTOs()
        {
            IEnumerable<RecruitmentDetailsDTO> result = DataContext.Recruitments
            .Include(x => x.Candidates)
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
                HiredCount = x.Candidates.Count(e => e.Status == CandidateStatuses.HIRED.ToString())
            }).ToList();

            return result;
        }

        public Recruitment GetRecruitmentDetails(int recruitmentId)
        {
            var result = DataContext.Recruitments
                .Include(x => x.Skills)
                .Where(x => x.Id == recruitmentId)
                .FirstOrDefault();

            return result;
        }
    }
}