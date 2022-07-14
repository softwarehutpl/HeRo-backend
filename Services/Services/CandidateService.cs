using AutoMapper;
using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using PagedList;
using Services.DTOs;
using Services.DTOs.Candidate;
using System.ComponentModel.DataAnnotations;

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
            ILogger<RecruitmentService>logger, RecruitmentRepository recruitmentRepository)
        {
            _mapper = map;
            _candidateRepository = candidateRepository;
            _recruitmentRepository = recruitmentRepository;
            _logger = logger;
        }
       
        public int CreateCandidate(CreateCandidateDTO dto)
        {
            //przekazanie wartości z DTO do obiektu
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

        public int ChangeStageAndStatus(int id, CandidateChangeStageAndStatusDTO dto)
        {
            Candidate? candidate = _candidateRepository.GetById(id);
            if(candidate!=null)
            { 
                candidate.Status = dto.Status;
                candidate.Stage = dto.Stage;
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
            else return -1;
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
            else return -1;
        }

        public CandidateProfileDTO? GetCandidateProfileById(int id)
        {
            Candidate candidate = _candidateRepository.GetById(id);
            CandidateProfileDTO? result = null;
            if (candidate != null) result = _mapper.Map<CandidateProfileDTO>(candidate);
            return result;
        }
       
        public IEnumerable<CandidateInfoForListDTO> GetCandidates(Paging paging, SortOrder sortOrder, CandidateFilteringDTO dto)
        {
            IEnumerable<Candidate> candidates = _candidateRepository.GetAll();

           
            if (!String.IsNullOrEmpty(dto.Status))
            {
                candidates = candidates.Where(s => s.Status.Contains(dto.Status));
            }
            if (!String.IsNullOrEmpty(dto.Stage))
            {
                candidates = candidates.Where(s => s.Stage.Contains(dto.Stage));
            } 
            if (dto.RecruiterId!=0)
            {
                candidates = candidates.Where(s => s.RecruiterId==dto.RecruiterId);
            } 
            if (dto.RecruitmentId!=0)
            {
                candidates = candidates.Where(s => s.RecruitmentId == dto.RecruitmentId);
            } 
            if (dto.TechId!=0)
            {
                candidates = candidates.Where(s => s.TechId == dto.TechId);
            }
            candidates = candidates.Where(s => s.DeletedById == null);
            foreach (KeyValuePair<string, string> sort in sortOrder.Sort)
            {
                if (sort.Value == "DESC")
                {
                    candidates = candidates.OrderByDescending(u => u.Name);
                }
                else
                {
                    candidates = candidates.OrderBy(s => s.Name);
                }
            }

            IEnumerable<Candidate> pagedCandidates = candidates.ToPagedList(paging.PageNumber, paging.PageSize);
            //Missing type map configuration or unsupported mapping.

            IEnumerable<CandidateInfoForListDTO> result = _mapper.Map<IEnumerable<CandidateInfoForListDTO>>(pagedCandidates);
            return result;
        }
        //nie ma tabeli oraz klasy dla rozmowy także stworzyłem jak narazie DTO dla
        //potrzebnych danych
        
        public async Task<List<InterviewDTO>> GetInterviews()
        {
            return null;
        }

        //public int AddRecruitmentToCandidate(int candId, int recrId)
        //{
        //    Candidate? candidate = GetCandidateById(candId);
        //    Recruitment? recruitment = GetRecruitmentById(recrId);
        //    if (candidate != null)
        //    {
        //        candidate.TechId = recrId;
        //        int result = _candidateRepository.UpdateCandidate(candidate);
        //        return result;
        //    }
        //    else return -1;
        //}

        public int AllocateRecruiterAndTech(int id, CandidateAssigneesDTO dto)
        {
            Candidate candidate = _candidateRepository.GetById(id);
            if (candidate != null)
            {
                if(dto.TechId!=null) candidate.TechId = dto.TechId;
                if(dto.RecruiterId!=null) candidate.RecruiterId = dto.RecruiterId;
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
            else return -1;
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
            else return -1;
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
            else return -1;
        }
    }
}
