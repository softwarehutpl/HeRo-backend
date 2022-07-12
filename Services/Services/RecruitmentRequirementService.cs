using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Services.Services
{
    public class RecruitmentRequirementService
    {
        //tabela przejściowa między kampanią rekrutacyjną a wymaganiami
        public async Task<List<RecruitmentSkill>> GetRequirements()
        {
            return null;
        }
        public void AddRequirement(int recruitmentId, int skillId, int level)
        {

        }
    }
}
