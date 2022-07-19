using AutoMapper;
using Common.Enums;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Recruitment;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.Listing;

namespace Services.Services
{
    [ScopedRegistration]
    public class RecruitmentService
    {
        private readonly IMapper _mapper;
        private readonly RecruitmentRepository _recruitmentRepo;
        private readonly RecruitmentSkillRepository _recruitmentSkillRepo;
        private readonly UserRepository _userRepo;
        private readonly ILogger<RecruitmentService> _logger;

        public RecruitmentService(IMapper map, RecruitmentRepository repo, UserRepository userRepo, ILogger<RecruitmentService> logger, RecruitmentSkillRepository recruitmentSkillRepo)
        {
            _mapper = map;
            _recruitmentRepo = repo;
            _userRepo = userRepo;
            _logger = logger;
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

                if (recruitment == null)
                {
                    return -1;
                }

                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.BeginningDate = dto.BeginningDate;
                recruitment.EndingDate = dto.EndingDate;
                recruitment.Name = dto.Name;
                recruitment.Description = dto.Description;
                recruitment.RecruiterId = dto.RecruiterId;
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                IEnumerable<RecruitmentSkill> newSkills = _mapper.Map<IEnumerable<RecruitmentSkill>>(dto.Skills);

                foreach (RecruitmentSkill newSkill in newSkills)
                {
                    RecruitmentSkill? oldSkill = recruitment.Skills
                        .FirstOrDefault(e => e.SkillId == newSkill.SkillId);

                    if (oldSkill != default && oldSkill.SkillLevel != newSkill.SkillLevel)
                    {
                        oldSkill.SkillLevel = newSkill.SkillLevel;
                    }
                    else if (oldSkill == default)
                    {
                        RecruitmentSkill skill = new RecruitmentSkill();
                        skill.RecruitmentId = recruitmentId;
                        skill.SkillId = newSkill.SkillId;
                        skill.SkillLevel = newSkill.SkillLevel;

                        recruitment.Skills.Add(skill);
                    }
                }

                var skillsIdsToRemove = new List<int>();

                foreach (RecruitmentSkill oldSkill in recruitment.Skills)
                {
                    RecruitmentSkill? newSkill = newSkills
                        .FirstOrDefault(e => e.SkillId == oldSkill.SkillId);

                    if (newSkill == default)
                    {
                        skillsIdsToRemove.Add(oldSkill.SkillId);
                    }
                }

                recruitment.Skills = (ICollection<RecruitmentSkill>)recruitment.Skills.
                    Where(e => !skillsIdsToRemove.Any(f => f == e.SkillId))
                    .ToList();

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

                if (recruitment == null) return -1;

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

                if (recruitment == null) return -1;

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

        public RecruitmentListing GetRecruitments(Paging paging, SortOrder? sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IQueryable<Recruitment> recruitments = _recruitmentRepo.GetAll();
            recruitments = recruitments.Where(r => !r.DeletedDate.HasValue);

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

            if (sortOrder != null && sortOrder.Sort != null)
            {
                recruitments = Sorter<Recruitment>.Sort(recruitments, sortOrder.Sort);
            }
            else
            {
                sortOrder = new SortOrder();
                sortOrder.Sort = new List<KeyValuePair<string, string>>();
                sortOrder.Sort.Add(new KeyValuePair<string, string>("Id", ""));

                recruitments = Sorter<Recruitment>.Sort(recruitments, sortOrder.Sort);
            }

            RecruitmentListing recruitmentListing = new RecruitmentListing();
            recruitmentListing.TotalCount = recruitments.Count();
            recruitmentListing.RecruitmentFiltringDTO = recruitmentFiltringDTO;
            recruitmentListing.Paging = paging;
            recruitmentListing.SortOrder = sortOrder;

            recruitmentListing.ReadRecruitmentDTOs = recruitments.Select(x => new ReadRecruitmentDTO
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
            }).ToPagedList(paging.PageNumber, paging.PageSize);

            return recruitmentListing;
        }

        public RecruitmentDetailsDTO GetRecruitment(int recruitmentId)
        {
            RecruitmentDetailsDTO? result;
            try
            {
                result = _recruitmentRepo.GetRecruitmentDTOById(recruitmentId);

                if (result == null)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }
    }
}