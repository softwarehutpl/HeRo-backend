using Data.DTOs.RecruitmentSkill;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CustomDataAnnotations
{
    public class NoDuplicatesAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            IEnumerable<RecruitmentSkillDTO> skills = value as IEnumerable<RecruitmentSkillDTO>;

            if (skills == null) return false;

            bool hasDuplicates = skills.Any(e => skills.Any(f => f.SkillId == e.SkillId && e!=f));

            if (hasDuplicates == true)
            {
                return false;
            }

            return true;
        }
    }
}
