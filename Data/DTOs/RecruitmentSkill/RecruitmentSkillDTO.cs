using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.RecruitmentSkill
{
    public class RecruitmentSkillDTO
    {
        public RecruitmentSkillDTO()
        {

        }
        public RecruitmentSkillDTO(int id, int level)
        {
            SkillId = id;
            SkillLevel = level;
        }
        public int SkillId { get; set; }

        public int SkillLevel { get; set; }
    }
}
