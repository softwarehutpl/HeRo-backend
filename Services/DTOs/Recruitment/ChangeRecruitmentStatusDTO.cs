using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Services.DTOs.Recruitment
{
    public class ChangeRecruitmentStatusDTO
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
    }
}
