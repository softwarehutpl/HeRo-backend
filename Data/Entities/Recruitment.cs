using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;
using Data.Repositories;

namespace Data.Entities
{
    [Table("Recruitment")]
    public class Recruitment : IEntity
    {

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Beginning date")]
        [Required(ErrorMessage = "This filed is required")]
        public DateTime BeginningDate { get; set; }

        [Display(Name = "Ending date")]
        [Required(ErrorMessage = "This field is required")]
        public DateTime EndingDate { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "This field is required")]
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
        public virtual User LastUpdatedBy { get; set; }

        public int? DeletedById { get; set; }

        public DateTime? DeletedDate { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User DeletedBy { get; set; }

        public int? EndedById { get; set; }

        [ForeignKey("EndedById")]
        public virtual User EndedBy { get; set; }
    }
}

