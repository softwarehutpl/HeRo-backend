using Microsoft.EntityFrameworkCore;

namespace Data.Entities.Report
{
    [Keyless]
    public class RequestedSkill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int Quantity { get; set; }
    }
}