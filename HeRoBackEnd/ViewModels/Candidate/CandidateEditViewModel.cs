using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Candidate
{
    public class CandidateEditViewModel
    {
        public int CandidateId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public int? ExpectedMonthlySalary { get; set; }
        public string? OtherExpectations { get; set; }
        public string? CvPath { get; set; }
        public string? Status { get; set; }
        public string? Stage { get; set; }
    }
}