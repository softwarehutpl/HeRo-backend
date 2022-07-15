using AutoMapper;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.DTOs.Candidate;
using Services.Listing;

namespace Services.Services
{
    [ScopedRegistration]
    public class CandidateService
    {
        private readonly CandidateRepository _candidateRepository;
        private readonly RecruitmentRepository _recruitmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecruitmentService> _logger;

        public CandidateService(IMapper map, CandidateRepository candidateRepository,
            ILogger<RecruitmentService> logger, RecruitmentRepository recruitmentRepository)
        {
            _mapper = map;
            _candidateRepository = candidateRepository;
            _recruitmentRepository = recruitmentRepository;
            _logger = logger;
        }

        public int GetQuantity()
        {
            int result = _candidateRepository.GetAll().Count();

            return result;
        }

        public int CreateCandidate(CreateCandidateDTO dto)
        {
            Candidate candidate = _mapper.Map<Candidate>(dto);
            candidate.RecruiterId = _recruitmentRepository.GetRecruiterId(candidate.RecruitmentId);

            try
            {
                _candidateRepository.AddAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        public int ChangeStatus(int id, string status)
        {
            Candidate? candidate = _candidateRepository.GetById(id);
            if (candidate != null)
            {
                candidate.Status = status;

                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    return -1;
                }

                return 1;
            }
            return -1;
        }

        public int UpdateCandidate(int id, UpdateCandidateDTO dto)
        {
            Candidate candidate = _candidateRepository.GetById(id);
            candidate.Name = dto.Name;
            candidate.LastName = dto.LastName;
            candidate.Status = dto.Status;
            candidate.Email = dto.Email;
            candidate.PhoneNumber = dto.PhoneNumber;
            candidate.AvailableFrom = dto.AvailableFrom;
            candidate.ExpectedMonthlySalary = dto.ExpectedMonthlySalary;
            candidate.OtherExpectations = dto.OtherExpectations;
            candidate.CvPath = dto.CvPath;

            try
            {
                _candidateRepository.UpdateAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        public int DeleteCandidate(DeleteCandidateDTO dto)
        {
            Candidate candidate = _candidateRepository.GetById(dto.Id);
            candidate.DeletedDate = dto.DeletedDate;
            candidate.DeletedById = dto.DeletedById;

            try
            {
                _candidateRepository.UpdateAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        public int AddHRNote(int candidateId, CandidateAddHRNoteDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = dto.RecruiterId;
                candidate.HROpinionText = dto.Note;
                candidate.HROpinionScore = dto.Score;

                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    return -1;
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int AddTechNote(int candidateId, CandidateAddTechNoteDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = dto.TechId;
                candidate.InterviewOpinionText = dto.Note;
                candidate.InterviewOpinionScore = dto.Score;

                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    return -1;
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        public CandidateProfileDTO? GetCandidateProfileById(int id)
        {
            Candidate candidate = _candidateRepository.GetById(id);

            CandidateProfileDTO candidateProfileDTO = new CandidateProfileDTO(
                                                    candidate.Id,
                                                    (candidate.Name + " " + candidate.LastName),
                                                    candidate.Email,
                                                    candidate.PhoneNumber,
                                                    candidate.AvailableFrom,
                                                    candidate.ExpectedMonthlySalary,
                                                    candidate.OtherExpectations,
                                                    candidate.CvPath
                                                );

            return candidateProfileDTO;
        }

        public CandidateListing GetCandidates(Paging paging, SortOrder sortOrder, CandidateFilteringDTO candidateFilteringDTO)
        {
            IQueryable<Candidate> candidates = _candidateRepository.GetAll();
            candidates = candidates.Where(s => !s.DeletedById.HasValue);

            if (candidateFilteringDTO.Status.Count > 0)
            {
                candidates = candidates.Where(c => candidateFilteringDTO.Status.Contains(c.Status));
            }
            if (candidateFilteringDTO.Stages.Count > 0)
            {
                candidates = candidates.Where(c => candidateFilteringDTO.Stages.Contains(c.Stage));
            }

            candidates = Sorter<Candidate>.Sort(candidates, sortOrder.Sort);

            CandidateListing candidateListing = new CandidateListing();
            candidateListing.TotalCount = candidates.Count();
            candidateListing.CandidateFilteringDTO = candidateFilteringDTO;
            candidateListing.Paging = paging;
            candidateListing.SortOrder = sortOrder;

            candidateListing.CandidateInfoForListDTOs = candidates
                .Select(c => new CandidateInfoForListDTO(
                            c.Id,
                           (c.Name + " " + c.LastName),
                            c.Source,
                            c.Recruitment.Name,
                            c.Status,
                            c.Stage,
                            c.TechId,
                            c.Tech.Email,
                            c.RecruiterId,
                            c.Recruiter.Email
                            )).ToPagedList(paging.PageNumber, paging.PageSize);

            return candidateListing;
        }

        public int AllocateRecruiterAndTech(int id, CandidateAssigneesDTO dto)
        {
            Candidate candidate = _candidateRepository.GetById(id);
            if (candidate != null)
            {
                if (dto.TechId != null)
                {
                    candidate.TechId = dto.TechId;
                }
                if (dto.RecruiterId != null)
                {
                    candidate.RecruiterId = dto.RecruiterId;
                }

                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    return -1;
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int AllocateRecruitmentInterview(int candidateId, CandidateAllocateInterviewDateDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = dto.LastUpdatedBy;
                candidate.InterviewDate = dto.Date;
                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    return -1;
                }

                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int AllocateTechInterview(int candidateId, CandidateAllocateInterviewDateDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);

            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = dto.LastUpdatedBy;
                candidate.TechInterviewDate = dto.Date;

                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}