using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Candidate")]
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
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime AplicationDate { get; set; }

        [Required]
        public int RecruitmentId { get; set; }

        [Required]
        public int RecruiterId { get; set; }

        [Required]
        public int TechId { get; set; }

        public DateTime InterviewDate { get; set; }

        public DateTime TechInterviewDate { get; set; }

        public string Notes { get; set; }

        [Required]
        public string CvPath { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        public int? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("LastUpdatedById")]
        public virtual User LastUpdatedBy { get; set; }

        public int? DeletedById { get; set; }

        public DateTime? DeletedDate { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User DeletedBy { get; set; }

        [ForeignKey("RecruiterId")]
        public virtual User Recruiter { get; set; }

        [ForeignKey("TechId")]
        public virtual User Tech { get; set; }

        [ForeignKey("RecruitmentId")]
        public virtual Recruitment Recruitment { get; set; }
    }
}
