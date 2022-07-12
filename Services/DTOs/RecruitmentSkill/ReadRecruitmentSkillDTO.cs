using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.RecruitmentSkill
{
    public class ReadRecruitmentSkillDTO
    {
        public int RecruitmentId { get; set; }

        public int SkillId { get; set; }

        public int SkillLevel { get; set; }
    }
}
