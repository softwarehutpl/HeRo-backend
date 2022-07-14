using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Candidate
{
    public class CandidateInfoForListDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Source { get; set; }
        public string Position { get; set; }

        public string Status { get; set; }
        public string? Stage { get; set; }
        public string TechAssignee { get; set; }
        public string RecruiterAssignee { get; set; }
        public int Id { get; set; }
    }
}
