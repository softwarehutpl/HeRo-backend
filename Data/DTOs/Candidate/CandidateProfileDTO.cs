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
        public string CvPath { get; set; }

        public CandidateProfileDTO(int id, string fullName, string email, string phoneNumber, DateTime? availableFrom, int? expectedMonthlySalary, string? otherExpectations, string cvPath)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            AvailableFrom = availableFrom;
            ExpectedMonthlySalary = expectedMonthlySalary;
            OtherExpectations = otherExpectations;
            CvPath = cvPath;
        }
    }
}