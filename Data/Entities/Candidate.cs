using Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Candidate")]
    public class Candidate : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName
        {
            get { return Name + " " + LastName; }
            private set { }
        }

        [Required]
        public string Status { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public int? ExpectedMonthlySalary { get; set; }

        public string? OtherExpectations { get; set; }

        public int? InterviewOpinionScore { get; set; }

        public string? InterviewOpinionText { get; set; }

        public int? HROpinionScore { get; set; }

        public string? HROpinionText { get; set; }

        public string? Source { get; set; }

        public string? Stage { get; set; }

        [Required]
        public int RecruitmentId { get; set; }

        public int? RecruiterId { get; set; }

        public int? TechId { get; set; }

        public DateTime? InterviewDate { get; set; }

        public DateTime? TechInterviewDate { get; set; }

        [Required]
        public string CvPath { get; set; }

        public int? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("LastUpdatedById")]
        public virtual User? LastUpdatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedById { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User? DeletedBy { get; set; }

        [ForeignKey("RecruiterId")]
        public virtual User? Recruiter { get; set; }

        [ForeignKey("TechId")]
        public virtual User? Tech { get; set; }

        [ForeignKey("RecruitmentId")]
        public virtual Recruitment Recruitment { get; set; }
    }
}