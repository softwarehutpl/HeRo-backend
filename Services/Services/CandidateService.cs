using AutoMapper;
using Data;
using Data.Entities;
using Data.Repositories;
using Services.DTOs;
using Services.DTOs.Candidate;
using System.ComponentModel.DataAnnotations;

namespace Services.Services
{
    public class CandidateService
    {
        private readonly CandidateRepository _candidateRepository;
        private readonly IMapper mapper;

        public CandidateService(IMapper map, CandidateRepository candidateRepository)
        {
            mapper = map;
            _candidateRepository = candidateRepository;
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
            Candidate candidate = mapper.Map<Candidate>(dto);

            int result = _candidateRepository.AddCandidate(candidate);

            return result;
        }
        public int ChangeStatus(int id, string status)
        {
            Candidate candidate = _candidateRepository.GetCandidateById(id);
            candidate.Status = status;
            int result = _candidateRepository.UpdateCandidate(candidate);

            return result;
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

            int result = _candidateRepository.UpdateCandidate(candidate);

            return result;
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

                int result = _candidateRepository.UpdateCandidate(candidate);

                return result;
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
                int result = _candidateRepository.UpdateCandidate(candidate);

                return result;

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
                int result = _candidateRepository.UpdateCandidate(candidate);

                return result;
            }
            else return -1;
        }
        public Candidate? GetCandidateById(int id)
        {
            Candidate candidate = _candidateRepository.GetCandidateById(id);
            return candidate;
        }
       
        public List<Candidate> GetCandidates()
        {
            return _candidateRepository.GetAllCandidates().ToList();
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

        public int AllocateTech(int id, int techId)
        {
            Candidate? candidate = GetCandidateById(id);
            if (candidate != null)
            {
                candidate.TechId = techId;
                int result = _candidateRepository.UpdateCandidate(candidate);
                return result;
            }
            else return -1;
        }
        public int AllocateRecruiter(int id, int recruiterId)
        {
            Candidate? candidate = GetCandidateById(id);
            if (candidate != null)
            {
                candidate.RecruiterId = recruiterId;
                int result = _candidateRepository.UpdateCandidate(candidate);
                return result;
            }
            else return -1;
        }
        public int AllocateRecruitmentInterview(int id, DateTime date)
        {
            Candidate? candidate = GetCandidateById(id);
            if (candidate != null)
            {
                candidate.InterviewDate = date;
                int result = _candidateRepository.UpdateCandidate(candidate);
                return result;
            }
            else return -1;
        }
        public int AllocateTechInterview(int id, DateTime date)
        {
            Candidate? candidate = GetCandidateById(id);
            if (candidate != null)
            {
                candidate.TechInterviewDate = date;
                int result = _candidateRepository.UpdateCandidate(candidate);
                return result;
            }
            else return -1;
            
        }
    }
}
