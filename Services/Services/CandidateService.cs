using AutoMapper;
using Common.Enums;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Candidate;
using Data.Entities;
using Data.IRepositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.Listing;

namespace Services.Services
{
    [ScopedRegistration]
    public class CandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IRecruitmentRepository _recruitmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RecruitmentService> _logger;

        public CandidateService(IMapper map, ICandidateRepository candidateRepository,
            ILogger<RecruitmentService> logger, IRecruitmentRepository recruitmentRepository)
        {
            _mapper = map;
            _candidateRepository = candidateRepository;
            _recruitmentRepository = recruitmentRepository;
            _logger = logger;
        }

        public int CreateCandidate(CreateCandidateDTO dto)
        {
            Candidate candidate;
            try
            {
                candidate = _mapper.Map<Candidate>(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when mapping CreateCandidateDTO to Candidate: " + ex);
                return -1;
            }

            candidate.Stage = StageNames.EVALUATION.ToString();
            int temp_result = _recruitmentRepository.GetRecruiterId(candidate.RecruitmentId);
            if (temp_result != 0)
                candidate.RecruiterId = temp_result;
            else
            {
                _logger.LogError("Error when getting recruiterId of recruitment which doesn't exist: ");
                return -2;
            }

            try
            {
                _candidateRepository.AddAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when adding and saving candidate: " + ex);
                return -3;
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
                    _logger.LogError("Error when changing candidate status: " + ex);
                    return -1;
                }
                return 1;
            }
            else
            {
                _logger.LogError("Error getting candidate with given ID");
                return -1;
            }
        }

        public int UpdateCandidate(int id, UpdateCandidateDTO dto)
        {
            Candidate candidate = _candidateRepository.GetById(id);

            if (candidate == null)
            {
                _logger.LogError("Error getting candidate with given ID");
                return -1;
            }
            else
            {
                candidate.Name = dto.Name;
                candidate.LastName = dto.LastName;
                candidate.Status = dto.Status;
                candidate.Stage = dto.Stage;
                candidate.Email = dto.Email;
                candidate.PhoneNumber = dto.PhoneNumber;
                candidate.AvailableFrom = dto.AvailableFrom;
                candidate.ExpectedMonthlySalary = dto.ExpectedMonthlySalary;
                candidate.OtherExpectations = dto.OtherExpectations;
                candidate.CvPath = dto.CvPath;
            }

            try
            {
                _candidateRepository.UpdateAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when updating candidate: " + ex);
                return -2;
            }

            return 1;
        }

        public int DeleteCandidate(DeleteCandidateDTO dto)
        {
            Candidate candidate = _candidateRepository.GetById(dto.Id);

            if (candidate == null)
            {
                _logger.LogError("Error removing candidate with given ID - candidate doesn't exist");
                return -1;
            }

            candidate.DeletedDate = dto.DeletedDate;
            candidate.DeletedById = dto.DeletedById;

            try
            {
                _candidateRepository.UpdateAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when deleting candidate: " + ex);
                return -2;
            }

            return 1;
        }

        public int AddHRNote(int candidateId, CandidateAddHRNoteDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate == null)
            {
                _logger.LogError("Error getting candidate with given ID");
                return -1;
            }
            else
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
                    _logger.LogError("Error when adding HR note to candidate: " + ex);
                    return -2;
                }

                return 1;
            }
        }

        public int AddTechNote(int candidateId, CandidateAddTechNoteDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate == null)
            {
                _logger.LogError("Error getting candidate with given ID");
                return -1;
            }
            else
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
                    _logger.LogError("Error when adding tech note to candidate: " + ex);
                    return -2;
                }

                return 1;
            }
        }

        public CandidateProfileDTO? GetCandidateProfileById(int id)
        {
            Candidate candidate;
            CandidateProfileDTO candidateProfileDTO;

            try
            {
                candidate = _candidateRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting candidate with given ID: " + ex);
                return null;
            }

            try
            {
                candidateProfileDTO = new CandidateProfileDTO(
                                                    candidate.Id,
                                                    (candidate.Name + " " + candidate.LastName),
                                                    candidate.Email,
                                                    candidate.PhoneNumber,
                                                    candidate.AvailableFrom,
                                                    candidate.ExpectedMonthlySalary,
                                                    candidate.OtherExpectations,
                                                    candidate.CvPath
                                                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating candidateProfileDTO (candidate == null ?): " + ex);
                return null;
            }

            return candidateProfileDTO;
        }

        public CandidateListing GetCandidates(Paging paging, SortOrder? sortOrder, CandidateFilteringDTO candidateFilteringDTO)
        {
            IQueryable<Candidate> candidates = _candidateRepository.GetAll();
            candidates = candidates.Where(s => !s.DeletedById.HasValue);

            if (candidateFilteringDTO.Status != null && candidateFilteringDTO.Status.Count > 0)
            {
                candidates = candidates.Where(c => candidateFilteringDTO.Status.Contains(c.Status));
            }
            if (candidateFilteringDTO.Stages != null && candidateFilteringDTO.Stages.Count > 0)
            {
                candidates = candidates.Where(c => candidateFilteringDTO.Stages.Contains(c.Stage));
            }

            if (sortOrder != null && sortOrder.Sort != null)
            {
                candidates = Sorter<Candidate>.Sort(candidates, sortOrder.Sort);
            }
            else
            {
                sortOrder = new SortOrder();
                sortOrder.Sort = new List<KeyValuePair<string, string>>();
                sortOrder.Sort.Add(new KeyValuePair<string, string>("Id", ""));

                candidates = Sorter<Candidate>.Sort(candidates, sortOrder.Sort);
            }

            CandidateListing candidateListing = new();
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
                if (dto.TechId != 0)
                {
                    candidate.TechId = dto.TechId;
                }

                if (dto.RecruiterId != 0)
                {
                    candidate.RecruiterId = dto.RecruiterId;
                }

                try
                {
                    _candidateRepository.UpdateAndSaveChanges(candidate);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error updating candidate when allocating recruiters: " + ex);
                    return -2;
                }

                return 1;
            }
            else
            {
                _logger.LogError("Cannot get candidate with given Id");
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
                    _logger.LogError("Error updating candidate when assigning interview date" + ex);
                    return -2;
                }

                return 1;
            }
            else
            {
                _logger.LogError("Cannot get candidate with given Id");
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
                    _logger.LogError("Error updating candidate when assigning tech interview date" + ex);
                    return -1;
                }

                return 1;
            }
            else
            {
                _logger.LogError("Cannot get candidate with given Id");
                return -1;
            }
        }
    }
}