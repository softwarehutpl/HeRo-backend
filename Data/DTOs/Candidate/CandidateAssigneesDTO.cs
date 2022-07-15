using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Candidate
{
    public class CandidateAssigneesDTO
    {
        public int Id { get; set; }
        public int TechId { get; set; }
        public int RecruiterId { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
