using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Candidate
{
    public class CandidateProfileDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public int? ExpectedMonthlySalary { get; set; }
        public string? OtherExpectations { get; set; }
        public string InterviewName { get; set; }
        public int? InterviewOpinionScore { get; set; }
        public string? InterviewOpinionText { get; set; }
        public string HRName { get; set; }
        public int? HROpinionScore { get; set; }
        public string? HROpinionText { get; set; }
    }
}