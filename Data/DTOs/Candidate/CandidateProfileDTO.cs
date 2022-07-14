using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Candidate
{
    //DTO do zawartości profilu
    public class CandidateProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime ApplicationDate { get; set; }

        //public string Languages { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public int? ExpectedMonthlySalary { get; set; }
        public string? OtherExpectations { get; set; }
        public int RecruitmentId { get; set; }
        public int RecruiterId { get; set; }
        public string CvPath { get; set; }
    }
}
