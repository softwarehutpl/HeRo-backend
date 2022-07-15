using AutoMapper;
using Common.Enums;
using Common.Listing;
using Data.DTO;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.DTOs.Recruitment;

namespace Services.Services
{
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

        public IEnumerable<ReadRecruitmentDTO> GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IEnumerable<ReadRecruitmentDTO> recruitments = repo.GetAllRecruitmentsDTOs();

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
                if (sort.Key.ToLower() == "name")
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
            }

            IEnumerable<ReadRecruitmentDTO> pagedRecruitments = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);

            return pagedRecruitments;
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