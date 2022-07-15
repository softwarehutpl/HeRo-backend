using AutoMapper;
using Common.Enums;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Data.DTOs.Recruitment;
using System.Security.Claims;
using Data.DTOs.RecruitmentSkill;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        private readonly IMapper _mapper;
        public RecruitmentRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public RecruitmentDetailsDTO GetRecruitmentById(int id)
        {
            var result = DataContext.Recruitments
                .Where(e => !e.DeletedDate.HasValue && e.Id == id)
                .Select(x => new RecruitmentDetailsDTO()
                {
                    Id = x.Id,
                    BeginningDate = x.BeginningDate,
                    EndingDate = x.EndingDate,
                    Name = x.Name,
                    Description = x.Description,
                    RecruiterId = x.RecruiterId,
                    Skills = x.Skills.Select(s => new RecruitmentSkillDetailsDTO()
                    {
                        SkillId = s.SkillId,
                        SkillLevel = s.SkillLevel,
                        Name = s.Skill.Name
                    }).ToList()
                })
                .FirstOrDefault();
                        
            return result;
        }

        public List<RecruitmentDetailsDTO> GetAllRecruitments()
        {
            List<RecruitmentDetailsDTO> result = GetAll()
                .Where(e => !(e.DeletedDate.HasValue))
                .Select(e => new RecruitmentDetailsDTO()
                {
                    Id = e.Id,
                    BeginningDate = e.BeginningDate,
                    EndingDate = e.EndingDate,
                    Name = e.Name,
                    Description = e.Description,
                    RecruiterId = e.RecruiterId
                })
                .ToList();               

            return result;
        }
        public int GetRecruiterId(int id)
        {
            var result = DataContext.Recruitments
                .Select(x => x.RecruiterId)
                .FirstOrDefault();
            
            return result;
        }
        public Recruitment GetById(int id)
        {
            Recruitment result = DataContext.Recruitments
                .Include(x => x.Skills)
                .FirstOrDefault(x => x.Id == id);

            return result;
        }
    }
}
