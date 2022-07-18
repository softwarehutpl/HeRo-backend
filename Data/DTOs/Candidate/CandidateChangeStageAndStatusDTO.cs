using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Candidate
{
    public class CandidateChangeStageAndStatusDTO
    {
        public string Status { get; set; }
        public string Stage { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
