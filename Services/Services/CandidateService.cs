using AutoMapper;
using Common.Listing;
using Data;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Services.DTOs;
using Services.DTOs.Candidate;
using System.ComponentModel.DataAnnotations;

namespace Services.Services
{
    public class CandidateService
    {
        private readonly CandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public CandidateService(IMapper map, CandidateRepository candidateRepository, DataContext dataContext)
        {
            _mapper = map;
            _candidateRepository = candidateRepository;
            _dataContext = dataContext;
        }
        private int GetUserId()
        {
            //jak narazie nie ma zalogowanego użytkownika, więc to nie działa
            /*List<Claim> claims = ClaimsPrincipal.Current.Claims.ToList();
            Claim emailClaim = claims.FirstOrDefault(e => e.Type == ClaimTypes.Email);
            User user = userRepo.GetUserByEmail(emailClaim.Value);

            return user;*/
            User user = new User();
            user.Id = 1;

            return user.Id;
        }
        public int CreateCandidate(CreateCandidateDTO dto)
        {
            //przekazanie wartości z DTO do obiektu
            Candidate candidate = _mapper.Map<Candidate>(dto);
            int recId = candidate.RecruitmentId;
            
            RecruitmentRepository recRepo  = new RecruitmentRepository(_dataContext);
            UserRepository userRepo = new UserRepository(_dataContext);
            RecruitmentService rs = new RecruitmentService(_mapper, recRepo, userRepo);
            candidate.RecruiterId = rs.GetRecruiterIdFromRecruitmentId(recId);
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
            Candidate candidate = _candidateRepository.GetCandidateById(id);
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
            candidate.LastUpdatedDate = DateTime.Now;
            candidate.LastUpdatedById = GetUserId();

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

        public int DeleteCandidate(int id)
        {
            int userId = GetUserId();

            Candidate? candidate = _candidateRepository.GetById(id);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = userId;
                candidate.DeletedDate = DateTime.Now;
                candidate.DeletedById = userId;

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

        public int AddHRNote(int id, string note, int score)
        {
            int userId = GetUserId();
            Candidate? candidate = _candidateRepository.GetById(id);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = userId;
                candidate.HROpinionText = note;
                candidate.HROpinionScore = score;
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

        public int AddInterviewNote(int id, string note, int score)
        {
            int userId = GetUserId();
            Candidate? candidate = _candidateRepository.GetById(id);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = userId;
                candidate.InterviewOpinionText = note;
                candidate.InterviewOpinionScore = score;
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
            Candidate candidate = _candidateRepository.GetCandidateById(id);
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
            int userId = GetUserId();
            Candidate candidate = _candidateRepository.GetCandidateById(id);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = userId;
                if (dto.TechId!=null) candidate.TechId = dto.TechId;
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
       
        public int AllocateRecruitmentInterview(int id, DateTime date)
        {
            int userId = GetUserId();
            Candidate? candidate = _candidateRepository.GetCandidateById(id);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = userId;
                candidate.InterviewDate = date;
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
        public int AllocateTechInterview(int id, DateTime date)
        {
            int userId = GetUserId();
            Candidate? candidate = _candidateRepository.GetCandidateById(id);
            if (candidate != null)
            {
                candidate.LastUpdatedDate = DateTime.Now;
                candidate.LastUpdatedById = userId;
                candidate.TechInterviewDate = date;
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
