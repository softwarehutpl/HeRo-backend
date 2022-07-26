using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Candidate
{
    public class CandidateCreateViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime AvailableFrom { get; set; }

        public int? ExpectedMonthlySalary { get; set; }

        public string? OtherExpectations { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public IFormFile CV { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int RecruitmentId { get; set; }
    }
}