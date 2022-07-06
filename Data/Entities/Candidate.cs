using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Candidate
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Email{ get; set; }
        [Required]
        public DateTime ApplicationDate  { get; set; }
        [ForeignKey("Recruitment")]
        public int RecruitmentId { get; set; }
        [Required]
        
        public string RecruiterId { get; set; }

        [Required]
        public string TechId  { get; set; }

        public DateTime InterviewDate { get; set; }
        
        public DateTime TechInterviewDate { get; set; }
        public string Notes { get; set; }
        [Required]
        public string CvPath { get; set; }
        [ForeignKey("TechId")]
        public virtual IdentityUser Tech { get; set; }
        [ForeignKey("RecruiterId")]
        public virtual IdentityUser Recruiter{ get; set; }


    }
}
