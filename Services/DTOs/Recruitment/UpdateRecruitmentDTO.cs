using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Services.DTOs.Recruitment
{
    public class UpdateRecruitmentDTO
    {
        [Display(Name = "Beginning date")]
        [Required(ErrorMessage = "This filed is required")]
        public DateTime BeginningDate { get; set; }

        [Display(Name = "Ending date")]
        [Required(ErrorMessage = "This field is required")]
        public DateTime EndingDate { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Display(Name = "Recruiter id")]
        [Required(ErrorMessage = "This field is required")]
        public string RecruiterId { get; set; }
        [Display(Name = "Status")]
        public RecruitmentStatusEnum Status { get; set; }
    }
}
