using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeRoBackEnd.Models
{
    [Table("Recruitment")]
    public class Recruitment
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Beginning date")]
        [Required(ErrorMessage = "This filed is required")]
        public DateTime BeginningDate { get; set; }

        [Display(Name = "Ending date")]
        [Required(ErrorMessage = "This field is required")]
        public DateTime EndingDate { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required")]
        public string name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is required")]
        public string description { get; set; }

        [Display(Name = "Tech id")]
        [ForeignKey("User")]
        [Required(ErrorMessage = "This field is required")]
        public int TechId { get; set; }

        [Display(Name = "Recruiter id")]
        [ForeignKey("User")]
        [Required(ErrorMessage = "This field is required")]
        public int RecruiterId { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }
    }
}

