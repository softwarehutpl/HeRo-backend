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

namespace Services.Services
{
    [ScopedRegistration]
    public class RecruitmentService
    {
        private readonly IMapper _mapper;
        private readonly RecruitmentRepository _recruitmentRepository;
        private readonly ILogger<RecruitmentService> _logger;

        public RecruitmentService(IMapper map, RecruitmentRepository repo, ILogger<RecruitmentService> logger)
        {
            _mapper = map;
            _recruitmentRepository = repo;
            _logger = logger;
        }

        public int AddRecruitment(CreateRecruitmentDTO dto)
        {
            try
            {
                Recruitment recruitment = _mapper.Map<Recruitment>(dto);

                _recruitmentRepository.AddAndSaveChanges(recruitment);
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
                Recruitment recruitment = _recruitmentRepository.GetById(recruitmentId);
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.BeginningDate = dto.BeginningDate;
                recruitment.EndingDate = dto.EndingDate;
                recruitment.Name = dto.Name;
                recruitment.Description = dto.Description;
                recruitment.RecruiterId = dto.RecruiterId;
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;

                _recruitmentRepository.UpdateAndSaveChanges(recruitment);
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
                Recruitment recruitment = _recruitmentRepository.GetById(dto.Id);
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.EndedDate = dto.EndedDate;
                recruitment.EndedById = dto.EndedById;

                _recruitmentRepository.UpdateAndSaveChanges(recruitment);
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
                Recruitment recruitment = _recruitmentRepository.GetById(dto.Id);
                recruitment.LastUpdatedDate = dto.LastUpdatedDate;
                recruitment.LastUpdatedById = dto.LastUpdatedById;
                recruitment.DeletedDate = dto.DeletedDate;
                recruitment.DeletedById = dto.LastUpdatedById;

                _recruitmentRepository.UpdateAndSaveChanges(recruitment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public IEnumerable<ReadRecruitmentDTO> GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IEnumerable<ReadRecruitmentDTO> recruitments = _recruitmentRepository.GetAllRecruitmentsDTOs();

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
                recruitment = _recruitmentRepository.GetRecruitmentDTOById(recruitmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            if (recruitment == null)
                return null;

            return recruitment;
        }
    }
}