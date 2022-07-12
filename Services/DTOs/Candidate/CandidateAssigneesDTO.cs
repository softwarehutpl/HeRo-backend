using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Candidate
{
    public class CandidateAssigneesDTO
    {
        public int Id { get; set; }
        public int TechId { get; set; }
        public int RecruiterId { get; set; }
    }
}
