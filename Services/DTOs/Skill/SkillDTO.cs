using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Skill
{
    public class SkillDTO
    {
        public SkillDTO()
        {

        }
        public SkillDTO(string name)
        {
            Name = name;
        }
        public SkillDTO(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
