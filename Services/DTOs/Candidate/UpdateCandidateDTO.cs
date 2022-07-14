using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Candidate
{
    public class UpdateCandidateDTO
    {
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime? AvailableFrom { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public int? ExpectedMonthlySalary { get; set; }
        public string? OtherExpectations { get; set; }

        public string CvPath { get; set; }
    }
}