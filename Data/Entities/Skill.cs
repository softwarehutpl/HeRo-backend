using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Skill
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string SkillName { get; set; }
    }
}
