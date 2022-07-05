using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Services.DTOs;

namespace Services.Services
{
    public class RecruitmentService
    {
        /// <summary>
        /// Adds a candidate to the database and to the recruitment
        /// </summary>
        /// <param name="dto">Takes an object of CandidateDTO class containing
        /// information about a candidate</param>
        /// <returns></returns>
        public async Task<int> AddCandidate(CandidateDTO dto)
        {
            return 0;
        }
        public async Task<int> AddRecruitment(RecruitmentDTO dto)
        {
            return 0;
        }
        public void UpdateRecruitment(int id, RecruitmentDTO dto)
        {

        }
        public void EndRecruitment(int id)
        {

        }
        public async Task<List<Recruitment>> GetRecruitments()
        {
            return null;
        }
    }
}
