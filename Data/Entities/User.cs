using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(40)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public string UserStatus { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        public int LastUpdatedById { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        [ForeignKey("LastUpdatedById")]
        public virtual User LastUpdatedBy { get; set; }

        public int DeletedById { get; set; }

        public DateTime DeletedDate { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User DeletedBy { get; set; }
    }
}
