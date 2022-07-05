using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        public string UserStatus { get; set; }
        public Role Role { get; set; }
    }
}
