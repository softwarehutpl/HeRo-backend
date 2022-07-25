using Data.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("UserAction")]
    public class UserAction : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? UserId { get; set; } 

        [Required]
        public DateTime Date { get; set; } 

        [Required]
        public string ControllerAction{ get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}