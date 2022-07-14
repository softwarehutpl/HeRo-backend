using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Candidate
{
    public class CandidateFilteringDTO
    {
       
       

        public string Status { get; set; }
        public string? Stage { get; set; }
        public int? RecruiterId { get; set; }
        public int? TechId { get; set; }
        public int RecruitmentId { get; set; }
        public CandidateFilteringDTO(string status, string? stage, int? recruiterId, int? techId, int recruitmentId)
        {

            Status = status;
            Stage = stage;
            RecruiterId = recruiterId;
            TechId = techId;
            RecruitmentId = recruitmentId;
        }
    }
}
