using HeRoBackEnd.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services
{
    internal class RecruitmentServices
    {
        private readonly DataContext context;
        public RecruitmentServices(DataContext context)
        {
            this.context = context;
        }

        public List<Recruitment> GetRecruitments()
        {
            return null;
        }
    }
}
