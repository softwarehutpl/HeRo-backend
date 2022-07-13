using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Recruitment
{
    public class RecruitmentEditViewModel
    {
        [Required(ErrorMessage = "This filed is required")]
        public DateTime BeginningDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime EndingDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int RecruiterId { get; set; }

        [MinLength(1, ErrorMessage = "You have to specify at least one skill")]
        public IEnumerable<RecruitmentSkill> Skills { get; set; }
    }
}
