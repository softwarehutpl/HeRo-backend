using Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("RecruitmentSkill")]
    public class RecruitmentSkill
    {
        public RecruitmentSkill(int recruitmentId, int skillId, int skillLevel)
        {
            this.RecruitmentId = recruitmentId;
            this.SkillId = skillId;
            this.SkillLevel = skillLevel;
        }
        public RecruitmentSkill()
        {

        }
        [Key]
        [Required(ErrorMessage = "Field is required!")]
        public int RecruitmentId { get; set; }

        [Key]
        [Required(ErrorMessage = "Field is required!")]
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Field is required!")]
        [Range(1, 5, ErrorMessage = "Value between 1 and 5 expected!")]
        public int SkillLevel { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }  

        [ForeignKey("RecruitmentId")]
        public virtual Recruitment Recruitment { get; set; }
    }
}
