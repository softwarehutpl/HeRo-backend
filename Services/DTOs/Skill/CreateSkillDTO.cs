using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Skill
{
    public class CreateSkillDTO
    {
        public CreateSkillDTO(string name, bool used)
        {
            Name = name;
            isUsed = used;
        }

        public string Name { get; set; }

        public bool isUsed { get; set; }
    }
}
