using Data.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Interview")]
    public class Interview : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public string Type { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? DeletedById { get; set; }

        public DateTime? DeletedDate { get; set; }

        [ForeignKey("CandidateId")]
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("WorkerId")]
        public virtual User User { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("LastUpdatedById")]
        public virtual User? LastUpdatedBy { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User? DeletedBy { get; set; }
    }
}