using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DTOs;

namespace Services.Services
{
    internal class CandidateServices
    {
        public void AddCandidate(CandidateDTO DTO)
        {

        }
        public void ChangeStatus(int id, string status)
        {

        }
        public void UpdateCandidate(int id, CandidateDTO DTO)
        {

        }
        public void DeleteCandidate(int id)
        {

        }
        public void AddNote(int id, string note)
        {

        }
        public async Task<List<Candidate>> GetCandidates()
        {
            return null;
        }
        //nie ma tabeli oraz klasy dla rozmowy także stworzyłem jak narazie DTO dla
        //potrzebnych danych
        public async Task<List<InterviewDTO>> GetInterviews()
        {
            return null;
        }
        public void AllocateTech(int id)
        {

        }
        public void AllocateRecruiter(int id)
        {

        }
        public void AllocateRecruitmentInterview(int id, DateTime date)
        {

        }
        public void AllocateTechInterview(int id, DateTime date)
        {

        }
    }
}
