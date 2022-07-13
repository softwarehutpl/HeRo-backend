using AutoMapper;
using Common.Listing;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.DTOs.Recruitment;
using System.Security.Claims;

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

                _recruitmentRepo.AddAndSaveChanges(recruitment);
            }
            catch(Exception ex)
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

                _recruitmentRepo.UpdateAndSaveChanges(recruitment);
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public IEnumerable<ReadRecruitmentDTO> GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            try
            {

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            IEnumerable<Recruitment> recruitments = _recruitmentRepo.GetAllRecruitments();
            recruitments = recruitments.Where(e => !(e.EndedDate.HasValue) && !(e.DeletedDate.HasValue));

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

            IEnumerable<Recruitment> pagedRecruitments = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);
            IEnumerable<ReadRecruitmentDTO> result = _mapper.Map<IEnumerable<ReadRecruitmentDTO>>(pagedRecruitments);

            return result;
        }

        public ReadRecruitmentDTO GetRecruitment(int recruitmentId)
        {
            Recruitment recruitment;
            ReadRecruitmentDTO result = null;
            try
            {
                recruitment = _recruitmentRepo.GetRecruitmentById(recruitmentId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            if (recruitment != null) result = _mapper.Map<ReadRecruitmentDTO>(recruitment);

            return result;
        }

        public int AddRrecruitmentSkills(IEnumerable<RecruitmentSkill> recruitmentSkills)
        {
            try
            {
                _recruitmentSkillRepo.AddRangeAndSaveChanges(recruitmentSkills);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int UpdateRecruitmentSkills(IEnumerable<RecruitmentSkill> recruitmentSkills)
        {
            try
            {
                IQueryable<RecruitmentSkill> oldRecruitmentSkills =
                    _recruitmentSkillRepo.GetAll().
                    Where(e => e.RecruitmentId == recruitmentSkills.First().RecruitmentId);

                _recruitmentSkillRepo.RemoveRangeAndSaveChanges(oldRecruitmentSkills);
                _recruitmentSkillRepo.AddRangeAndSaveChanges(recruitmentSkills);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return -1;
            }

            return 1;
        }

        public IEnumerable<RecruitmentSkill> GetAllRecruitmentSkills(int recruitmentId)
        {
            IEnumerable<RecruitmentSkill> result =
                _recruitmentSkillRepo.GetAll().
                Where(e => e.RecruitmentId == recruitmentId);

            return result;
        }
    }
}
