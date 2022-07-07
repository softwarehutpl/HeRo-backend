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
        public int AddRecruitment(CAndURecruitmentDTO dto)
        {
            return 0;
        }
        public void UpdateRecruitment(int recruitmentId, CAndURecruitmentDTO dto)
        {

        }
        public void ChangeStatus(int recruitmentId, string status)
        {

        }
        public void EndRecruitment(int recruitmentId)
        {

        }
        public ReadRecruitmentDTO GetRecruitment(int recruitmentId)
        {
            return null;
        }
        public List<ReadRecruitmentDTO> GetRecruitments()
        {
            return null;
        }
    }
}
