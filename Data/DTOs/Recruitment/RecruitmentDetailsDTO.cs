using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Common.Enums;
using Data.DTOs.RecruitmentSkill;

namespace Data.DTOs.Recruitment
{
    public class RecruitmentDetailsDTO
    {
        public int Id { get; set; }

        public DateTime BeginningDate { get; set; }

        public DateTime EndingDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RecruiterId { get; set; }

        public IEnumerable<RecruitmentSkillDetailsDTO> Skills { get; set; }
    }
}
