using Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("User")]
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(40)]
        public string Email { get; set; }

        public Guid PasswordRecoveryGuid { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)] 
     
        public string Password { get; set; }

        [Required]
        public string UserStatus { get; set; }

        public string RoleName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? DeletedById { get; set; }

        public DateTime? DeletedDate { get; set; }

        [ForeignKey("DeletedById")]
        public virtual User? DeletedBy { get; set; }
    }
}
