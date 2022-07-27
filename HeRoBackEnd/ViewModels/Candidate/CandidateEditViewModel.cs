using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Candidate
{
    public class CandidateEditViewModel
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
        public string Status { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Stage { get; set; }
    }
}