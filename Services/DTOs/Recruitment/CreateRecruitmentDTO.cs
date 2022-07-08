using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Services.DTOs.Recruitment
{
    public class CreateRecruitmentDTO
    {
        public DateTime BeginningDate { get; set; }

        public DateTime EndingDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string RecruiterId { get; set; }
    }
}
