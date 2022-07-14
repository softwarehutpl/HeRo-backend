using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Candidate
{
    public class CandidateCreateViewModel
    {
        [Required(ErrorMessage = "This filed is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "This filed is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public DateTime? AvailableFrom { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public int? ExpectedMonthlySalary { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public string? OtherExpectations { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public string CvPath { get; set; }

        [Required(ErrorMessage = "This filed is required")]
        public int RecruitmentId { get; set; }
    }
}
