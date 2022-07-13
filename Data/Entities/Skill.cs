using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Repositories;

namespace Data.Entities
{
    [Table("Skill")]
    public class Skill : IEntity
    {
        public Skill(string name)
        {
            Name = name;
        }
        public Skill()
        {

        }
        [Key]
        [Required(ErrorMessage = "Field is required!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field is required!")]
        [MaxLength(75, ErrorMessage = "Name of field is to long (max. 75 characters!")]
        public string Name { get; set; }
    }
}
