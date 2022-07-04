using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeRoBackEnd.Models
{
    [Table("CampaignRequirement")]
    public class CampaignRequirement
    {
        [Key]
        [ForeignKey("Campaign")]
        [Required(ErrorMessage = "Field is required!")]
        public int CampaignId { get; set; }

        [Key]
        [ForeignKey("Skill")]
        [Required(ErrorMessage = "Field is required!")]
        public int SkillId { get; set; }


        [Required(ErrorMessage = "Field is required!")]
        [MaxLength(75, ErrorMessage = "Name of field is to long (max. 75 characters!")]
        public string Name { get; set; }

        //public virtual Skill Skill {get;set;}
        //public virtual Campaign Campaign {get;set;}
    }
}
