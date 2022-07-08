using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Recruitment
{
    public class RecruitmentEditViewModel
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

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int RecruiterId { get; set; }
    }
}
