using AutoMapper;
using Common.Enums;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTO;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.DTOs.Recruitment;
using Services.Listing;

namespace Services.Services
{
    [ScopedRegistration]
    public class RecruitmentService
    {
        private readonly IMapper mapper;
        private readonly RecruitmentRepository repo;
        private readonly UserRepository userRepo;
        private readonly ILogger<RecruitmentService> logger;

        public RecruitmentService(IMapper map, RecruitmentRepository repo, UserRepository userRepo, ILogger<RecruitmentService> logger)
        {
            mapper = map;
            this.repo = repo;
            this.userRepo = userRepo;
            this.logger = logger;
        }

        public int AddRecruitment(CreateRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = mapper.Map<Recruitment>(dto);

                repo.AddAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int UpdateRecruitment(int recruitmentId, UpdateRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = repo.GetById(recruitmentId);
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.BeginningDate = dto.BeginningDate;
                recruitment.EndingDate = dto.EndingDate;
                recruitment.Name = dto.Name;
                recruitment.Description = dto.Description;
                recruitment.RecruiterId = dto.RecruiterId;
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;

                repo.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int EndRecruitment(EndRecruimentDTO dto)
        {
            try
            {
                Recruitment recruitment = repo.GetById(dto.Id);
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.EndedDate = dto.EndedDate;
                recruitment.EndedById = dto.EndedById;

                repo.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int DeleteRecruitment(DeleteRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = repo.GetById(dto.Id);
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.DeletedDate = dto.DeletedDate;
                recruitment.DeletedById = dto.LastUpdatedById;

                repo.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public RecruitmentListing GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IQueryable<Recruitment> recruitments = repo.GetAll();
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

            recruitments = Sorter<Recruitment>.Sort(recruitments, sortOrder.Sort);

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

        public ReadRecruitmentDTO? GetRecruitment(int recruitmentId)
        {
            ReadRecruitmentDTO? recruitment;

            try
            {
                recruitment = repo.GetRecruitmentDTOById(recruitmentId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }

            if (recruitment == null)
                return null;

            return recruitment;
        }
    }
}