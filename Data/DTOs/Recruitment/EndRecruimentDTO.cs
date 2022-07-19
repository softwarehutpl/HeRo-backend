using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Recruitment
{
    public class EndRecruimentDTO
    {
        public EndRecruimentDTO(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? EndedById { get; set; }
        public DateTime? EndedDate { get; set; }
    }
}
