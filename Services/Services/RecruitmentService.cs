using AutoMapper;
using Common.Listing;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Data.DTOs.Recruitment;
using System.Security.Claims;
using Data.DTOs.RecruitmentSkill;

namespace Services.Services
{
    public class RecruitmentService
    {
        private readonly IMapper _mapper;
        private readonly RecruitmentRepository _recruitmentRepo;
        private readonly RecruitmentSkillRepository _recruitmentSkillRepo;
        private readonly ILogger<RecruitmentService> _logger;
        public RecruitmentService(IMapper map, RecruitmentRepository repo, RecruitmentSkillRepository recruitmentSkillRepo, ILogger<RecruitmentService> logger)
        {
            _mapper = map;
            this._recruitmentRepo = repo;
            this._recruitmentSkillRepo = recruitmentSkillRepo;
            this._logger = logger;
        }

        public int AddRecruitment(CreateRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = _mapper.Map<Recruitment>(dto);
                IEnumerable<RecruitmentSkill> skills = _mapper.Map<IEnumerable<RecruitmentSkill>>(dto.Skills);
                recruitment.Skills = (ICollection<RecruitmentSkill>)skills;

                _recruitmentRepo.AddAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }
        public int UpdateRecruitment(int recruitmentId, UpdateRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = _recruitmentRepo.GetById(recruitmentId);
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.BeginningDate = dto.BeginningDate;
                recruitment.EndingDate = dto.EndingDate;
                recruitment.Name = dto.Name;
                recruitment.Description = dto.Description;
                recruitment.RecruiterId = dto.RecruiterId;
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                IEnumerable<RecruitmentSkill> skills = _mapper.Map<IEnumerable<RecruitmentSkill>>(dto.Skills);

                foreach (RecruitmentSkill skill in skills)
                {
                    skill.RecruitmentId = recruitmentId;
                }

                recruitment.Skills = (ICollection<RecruitmentSkill>)skills;



                DeleteRecruitmentSkills(recruitmentId);
                _recruitmentRepo.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int EndRecruitment(EndRecruimentDTO dto)
        {
            try
            {
                Recruitment recruitment = _recruitmentRepo.GetById(dto.Id);
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.EndedDate = dto.EndedDate;
                recruitment.EndedById = dto.EndedById;

                _recruitmentRepo.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int DeleteRecruitment(DeleteRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = _recruitmentRepo.GetById(dto.Id);
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.DeletedDate = dto.DeletedDate;
                recruitment.DeletedById = dto.LastUpdatedById;

                _recruitmentRepo.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public IEnumerable<RecruitmentDetailsDTO> GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IEnumerable<RecruitmentDetailsDTO> result;
            try
            {
                IEnumerable<RecruitmentDetailsDTO> recruitments = _recruitmentRepo.GetAllRecruitments();


                if (!String.IsNullOrEmpty(recruitmentFiltringDTO.Name))
                {
                    recruitments = recruitments.Where(s => s.Name.Contains(recruitmentFiltringDTO.Name));
                }
                if (!String.IsNullOrEmpty(recruitmentFiltringDTO.Description))
                {
                    recruitments = recruitments.Where(s => s.Description.Contains(recruitmentFiltringDTO.Description));
                }
                if (recruitmentFiltringDTO.BeginningDate.HasValue)
                {
                    recruitments = recruitments.Where(s => s.BeginningDate >= recruitmentFiltringDTO.BeginningDate);
                }
                if (recruitmentFiltringDTO.EndingDate.HasValue)
                {
                    recruitments = recruitments.Where(s => s.EndingDate <= recruitmentFiltringDTO.EndingDate);
                }


                foreach (KeyValuePair<string, string> sort in sortOrder.Sort)
                {
                    if (sort.Value == "DESC")
                    {
                        recruitments = recruitments.OrderByDescending(u => u.Name);
                    }
                    else
                    {
                        recruitments = recruitments.OrderBy(s => s.Name);
                    }
                }

                result = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);

                foreach (RecruitmentDetailsDTO dto in result)
                {
                    dto.Skills = GetAllRecruitmentSkills(dto.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }


            return result;
        }

        public RecruitmentDetailsDTO GetRecruitment(int recruitmentId)
        {
            RecruitmentDetailsDTO result = null;
            try
            {
                result = _recruitmentRepo.GetRecruitmentById(recruitmentId);
                result.Skills = GetAllRecruitmentSkills(result.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }

        public void DeleteRecruitmentSkills(int recruitmentId)
        {
            IQueryable<RecruitmentSkill> oldRecruitmentSkills =
                _recruitmentSkillRepo.GetAll().
                Where(e => e.RecruitmentId == recruitmentId);

            _recruitmentSkillRepo.RemoveRangeAndSaveChanges(oldRecruitmentSkills);
        }

        public IEnumerable<RecruitmentSkillDTO> GetAllRecruitmentSkills(int recruitmentId)
        {
            IEnumerable<RecruitmentSkillDTO> result =
                _recruitmentSkillRepo.GetAll().
                Where(e => e.RecruitmentId == recruitmentId).
                Select(e => _mapper.Map<RecruitmentSkillDTO>(e));

            return result;
        }
    }
}
