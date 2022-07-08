using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Recruitment
{
    public class RecruitmentChangeStatusViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "This field is required")]
        public int Id { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "This field is required")]
        public string Status { get; set; }
    }
}
