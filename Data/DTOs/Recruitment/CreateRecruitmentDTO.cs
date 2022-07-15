using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Data.DTOs.RecruitmentSkill;

namespace Data.DTOs.Recruitment
{
    public class CreateRecruitmentDTO
    {
        public DateTime BeginningDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecruiterId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string RecruitmentPosition { get; set; }
        public string Localization { get; set; }
        public string Seniority { get; set; }

        public IEnumerable<RecruitmentSkillDTO> Skills { get; set; }
    }
}