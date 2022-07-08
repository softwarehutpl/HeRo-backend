using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Recruitment
{
    public class RecruitmentChangeStatusViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Status { get; set; }
    }
}
