using Data.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Data.Entities
{
    [Table("User")]
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }

            private set
            {
            }
        }

        [Required]
        [EmailAddress]
        [MaxLength(40)]
        public string Email { get; set; }

        public Guid PasswordRecoveryGuid { get; set; }

        public Guid ConfirmationGuid { get; set; }

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