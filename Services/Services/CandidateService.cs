using AutoMapper;
using Common.Enums;
using Common.Helpers;
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

        public bool CreateCandidate(CreateCandidateDTO dto, out string ErrorMessage)
        {
            Candidate candidate;
            try
            {
                candidate = _mapper.Map<Candidate>(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when mapping CreateCandidateDTO to Candidate: " + ex);
                ErrorMessage = ErrorMessageHelper.InvalidCandidateParameters;
                return false;
            }

            candidate.Stage = StageNames.EVALUATION.ToString();
            int temp_result = _recruitmentRepository.GetRecruiterId(candidate.RecruitmentId);
            if (temp_result != 0)
                candidate.RecruiterId = temp_result;
            else
            {
                _logger.LogError("Error when getting recruiterId of recruitment which doesn't exist: ");

                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return false;
            }

            try
            {
                _candidateRepository.AddAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when adding and saving candidate: " + ex);
                ErrorMessage = ErrorMessageHelper.ErrorSavingToDatabase;
                return false;
            }
            ErrorMessage = "";
            return true;
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

        public bool UpdateCandidate(UpdateCandidateDTO updateCandidate, out string ErrorMessage)
        {
            Candidate? candidate = _candidateRepository.GetById(updateCandidate.CandidateId);

            if (candidate == null)
            {
                _logger.LogError("Error getting candidate with given ID");
                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return false;
            }
            else
            {
                if (updateCandidate.Name != null)
                {
                    candidate.Name = updateCandidate.Name;
                }
                if (updateCandidate.LastName != null)
                {
                    candidate.LastName = updateCandidate.LastName;
                }
                if (updateCandidate.Status != null)
                {
                    candidate.Status = updateCandidate.Status;
                }
                if (updateCandidate.Stage != null)
                {
                    candidate.Stage = updateCandidate.Stage;
                }
                if (updateCandidate.Email != null)
                {
                    candidate.Email = updateCandidate.Email;
                }
                if (updateCandidate.PhoneNumber != null)
                {
                    candidate.PhoneNumber = updateCandidate.PhoneNumber;
                }
                if (updateCandidate.ExpectedMonthlySalary != null)
                {
                    candidate.ExpectedMonthlySalary = updateCandidate.ExpectedMonthlySalary;
                }
                if (updateCandidate.OtherExpectations != null)
                {
                    candidate.OtherExpectations = updateCandidate.OtherExpectations;
                }
                if (updateCandidate.CV != null)
                {
                    candidate.CV = updateCandidate.CV;
                }

                candidate.LastUpdatedById = updateCandidate.LastUpdatedBy;
                candidate.LastUpdatedDate = updateCandidate.LastUpdatedDate;
            }

            try
            {
                _candidateRepository.UpdateAndSaveChanges(candidate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when updating candidate: " + ex);
                ErrorMessage = ErrorMessageHelper.ErrorUpdatingCandidate;
                return false;
            }
            ErrorMessage = "";
            return true;
        }

        public bool DeleteCandidate(DeleteCandidateDTO dto, out string ErrorMessage)
        {
            Candidate candidate = _candidateRepository.GetById(dto.Id);

            if (candidate == null)
            {
                _logger.LogError("Error removing candidate with given ID - candidate doesn't exist");
                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return false;
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
                ErrorMessage = ErrorMessageHelper.ErrorDeletingCandidate;
                return false;
            }
            ErrorMessage = "";
            return true;
        }

        public bool AddHRNote(int candidateId, CandidateAddHRNoteDTO dto, out string ErrorMessage)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate == null)
            {
                _logger.LogError("Error getting candidate with given ID");
                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return false;
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
                    ErrorMessage = ErrorMessageHelper.ErrorAddingHRNoteToCandidate;
                    return false;
                }
                ErrorMessage = "";
                return true;
            }
        }

        public bool AddTechNote(int candidateId, CandidateAddTechNoteDTO dto, out string ErrorMessage)
        {
            Candidate? candidate = _candidateRepository.GetById(candidateId);
            if (candidate == null)
            {
                _logger.LogError("Error getting candidate with given ID");
                ErrorMessage = ErrorMessageHelper.InvalidCandidateId;
                return false;
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
                    ErrorMessage = ErrorMessageHelper.ErrorAddingTechNoteToCandidate;
                    return false;
                }
                ErrorMessage = "";
                return true;
            }
        }

        public CandidateProfileDTO? GetCandidateProfileById(int id, out string ErrorMessage)
        {
            Candidate candidate;
            CandidateProfileDTO candidateProfileDTO;

            try
            {
                candidate = _candidateRepository.GetCandidate(id);
                if (candidate == null)
                {
                    _logger.LogError("Error getting candidate with given ID");
                    ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting candidate with given ID: " + ex);
                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return null;
            }

            try
            {
                if (candidate.Tech == null)
                {
                    candidate.Tech = new User();
                }
                if (candidate.Recruiter == null)
                {
                    candidate.Recruiter = new User();
                }

                candidateProfileDTO =
                    new CandidateProfileDTO
                    {
                        Id = candidate.Id,
                        FullName = (candidate.Name + " " + candidate.LastName),
                        Email = candidate.Email,
                        PhoneNumber = candidate.PhoneNumber,
                        AvailableFrom = candidate.AvailableFrom,
                        ExpectedMonthlySalary = candidate.ExpectedMonthlySalary,
                        OtherExpectations = candidate.OtherExpectations,
                        InterviewName = candidate.Tech.FullName,
                        InterviewOpinionScore = candidate.InterviewOpinionScore,
                        InterviewOpinionText = candidate.InterviewOpinionText,
                        HRName = candidate.Recruiter.FullName,
                        HROpinionScore = candidate.HROpinionScore,
                        HROpinionText = candidate.HROpinionText
                    };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating candidateProfileDTO (candidate == null ?): " + ex);
                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return null;
            }
            ErrorMessage = "";
            return candidateProfileDTO;
        }
        public MemoryStream GetCandidateCV(int candidateId)
        {
            byte[] result;
            try
            {
                result = _candidateRepository.GetById(candidateId).CV;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting candidate with given ID: " + ex);
                return null;
            }

            var dataStream = new MemoryStream(result);

            return dataStream;
        }

        public CandidateListing GetCandidates(Paging paging, SortOrder? sortOrder, CandidateFilteringDTO candidateFilteringDTO)
        {
            IQueryable<Candidate> candidates = _candidateRepository.GetAll();
            candidates = candidates.Where(c => !c.DeletedById.HasValue);

            if (candidateFilteringDTO.RecruitmentId.HasValue && candidateFilteringDTO.RecruitmentId > 0)
            {
                candidates = candidates.Where(c => c.RecruitmentId == candidateFilteringDTO.RecruitmentId);
            }
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
                            $"{c.Tech.Name} {c.Tech.Surname}",
                            c.RecruiterId,
                            $"{c.Recruiter.Name} {c.Recruiter.Surname}"
                            )).ToPagedList(paging.PageNumber, paging.PageSize);

            return candidateListing;
        }

        public bool AllocateRecruiterAndTech(int id, CandidateAssigneesDTO dto, out string ErrorMessage)
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
                    ErrorMessage = ErrorMessageHelper.ErrorAllocatingRecruiters;
                    return false;
                }

                ErrorMessage = "";
                return true;
            }
            else
            {
                _logger.LogError("Cannot get candidate with given Id: " + id);
                ErrorMessage = ErrorMessageHelper.ErrorGettingCandidate;
                return false;
            }
        }
    }
}