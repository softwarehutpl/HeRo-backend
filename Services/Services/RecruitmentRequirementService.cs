using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    internal class RecruitmentRequirementService
    {
        //tabela przejściowa między kampanią rekrutacyjną a wymaganiami
        public async Task<List<RecruitmentRequirement>> GetRequirements()
        {
            return null;
        }
        public void AddRequirement(int recruitmentId, int skillId, int level)
        {

        }
    }
}
