using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Services.DTOs;

namespace Services.Services
{
    internal class RecruitmentServices
    {
        //void bo zakładam, że dane będą poddawane walidacji zanim zostaną przesłane
        //nie mam powodu zwrócić kodu błędu, ale też async void nie jest zalecane także
        //nie mogę użyć metod asynchronicznych
        public void AddRecruitment(RecruitmentDTO DTO)
        {
            
        }
        public void UpdateRecruitment(int id, RecruitmentDTO DTO)
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
