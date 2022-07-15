using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Recruitment
{
    public class DeleteRecruitmentDTO
    {
        public DeleteRecruitmentDTO(int id)
        {
            Id = id;
        }
        public int Id { get; set; }

        public int? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? DeletedById { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
