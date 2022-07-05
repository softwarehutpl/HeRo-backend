using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Services.DTOs;

namespace Services.Services
{
    internal class RecruitmentService
    {
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
