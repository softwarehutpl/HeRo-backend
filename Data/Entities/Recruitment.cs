using Data.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Recruitment")]
    public class Recruitment : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime BeginningDate { get; set; }

        public DateTime EndingDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RecruiterId { get; set; }

        [ForeignKey("RecruiterId")]
        public virtual User Recruiter { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        public int? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey("LastUpdatedById")]
        public virtual User? LastUpdatedBy { get; set; }

        public int? DeletedById { get; set; }

        public DateTime? DeletedDate { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User? DeletedBy { get; set; }

        public int? EndedById { get; set; }

        public DateTime? EndedDate { get; set; }

        [ForeignKey("EndedById")]
        public virtual User? EndedBy { get; set; }

        public string? RecruitmentPosition { get; set; }

        public string Localization { get; set; }

        public string Seniority { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<RecruitmentSkill> Skills { get; set; }
    }
}