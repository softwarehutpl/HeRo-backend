using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class CampainRequirement
    {

        [Required]
        public Recruitment Recruitment { get; set; }
        [Required]
        public Skill Skill { get; set; }
        [Required]
        public int Level { get; set; }
    }
}
