using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("CampaignRequirement")]
    public class RecruitmentRequirement
    {
        [Key]
        [ForeignKey("Campaign")]
        [Required(ErrorMessage = "Field is required!")]
        public int RecruitmentId { get; set; }

        [Key]
        [ForeignKey("Skill")]
        [Required(ErrorMessage = "Field is required!")]
        public int SkillId { get; set; }


        [Required(ErrorMessage = "Field is required!")]
        [MaxLength(75, ErrorMessage = "Name of field is to long (max. 75 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required!")]
        [Range(1,5, ErrorMessage="Value between 1 and 5 expected!")]
        public int SkillLevel { get; set; }

        public virtual Skill Skill {get;set;}
        public virtual Recruitment Recruitment {get;set;}
    }
}
