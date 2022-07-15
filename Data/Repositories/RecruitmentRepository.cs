using Common.Enums;
using Common.ServiceRegistrationAttributes;
using Data.DTO;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        public RecruitmentRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Recruitment> GetAllRecruitments()
        {
            var result = GetAll();

            return result;
        }

        public int GetRecruiterId(int id)
        {
            Recruitment recruitment = GetById(id);

            int result = recruitment.RecruiterId;

            return result;
        }

        public ReadRecruitmentDTO? GetRecruitmentDTOById(int id)
        {
            ReadRecruitmentDTO? result = DataContext.Recruitments
                .Include(x => x.Candidates)
                .Where(x => x.Id == id)
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
                })
                .FirstOrDefault();

            if (result == default) return null;

            return result;
        }

        public IEnumerable<ReadRecruitmentDTO> GetAllRecruitmentsDTOs()
        {
            IEnumerable<ReadRecruitmentDTO> result = DataContext.Recruitments
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