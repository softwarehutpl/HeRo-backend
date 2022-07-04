using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;

namespace HeRoBackEnd.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        [Required(ErrorMessage = "Field is required!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field is required!")]
        [MaxLength(75, ErrorMessage = "Name of field is to long (max. 75 characters!")]
        public Enum Name { get; set; }

        //public virtual ICollection<RecruitmentRequirement> RecruitmentRequirements {get;set;}
    }
}

//dodać enumy statusów 