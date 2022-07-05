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
        public DateTime AplicationDate  { get; set; }
        [ForeignKey("Recruitment")]
        public int RecruitmentId { get; set; }
        [Required]
        public User RecrouterId { get; set; }
        [Required]
        [ForeignKey("User")]
        public int TechId  { get; set; }

        public DateTime InterviewDate { get; set; }
        
        public DateTime TechInterviewDate { get; set; }
        public string Notes { get; set; }
        [Required]
        public string CvPath { get; set; }



    }
}
