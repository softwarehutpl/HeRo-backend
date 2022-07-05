using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class RecruitmentRequirement
    {

        [Required]
        public Recruitment Recruitment { get; set; }
        [Required]
        public Skill Skill { get; set; }
        [Required]
        public int Level { get; set; }
    }
}
