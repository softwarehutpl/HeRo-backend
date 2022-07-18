using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Candidate
{
    public class CandidateAddHRNoteDTO
    {
        public string Note { get; set; }
        public int Score { get; set; }
        public int RecruiterId { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
