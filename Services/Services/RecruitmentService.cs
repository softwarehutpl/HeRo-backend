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

        public int GetQuantity()
        {
            int result = repo.GetAll().Count();

            return result;
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
            try
            {
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
            IEnumerable<Recruitment> recruitments = repo.GetAllRecruitments();
            recruitments = recruitments.Where(e => !e.DeletedDate.HasValue);
            IEnumerable<Recruitment> recruitments = repo.GetAllRecruitments();
            recruitments = recruitments.Where(e => !e.DeletedDate.HasValue);
            IEnumerable<Recruitment> recruitments = repo.GetAllRecruitments();
            recruitments = recruitments.Where(e => !e.DeletedDate.HasValue);
            IEnumerable<Recruitment> recruitments = repo.GetAllRecruitments();
            recruitments = recruitments.Where(e => !e.DeletedDate.HasValue);
            IEnumerable<Recruitment> recruitments = repo.GetAllRecruitments();
            recruitments = recruitments.Where(e => !e.DeletedDate.HasValue);

            IQueryable<Recruitment> recruitments = repo.GetAllRecruitments();
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

            IEnumerable<Recruitment> pagedRecruitments = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);
            recruitmentListing.ReadRecruitmentDTOs = mapper.Map<IEnumerable<ReadRecruitmentDTO>>(pagedRecruitments);
            RecruitmentListing recruitmentListing = new RecruitmentListing();
            return recruitmentListing;
            recruitmentListing.RecruitmentFiltringDTO = recruitmentFiltringDTO;
            recruitmentListing.Paging = paging;
            recruitmentListing.SortOrder = sortOrder;

            IEnumerable<Recruitment> pagedRecruitments = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);
            IEnumerable<ReadRecruitmentDTO> result = mapper.Map<IEnumerable<ReadRecruitmentDTO>>(pagedRecruitments);

            return result;
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