using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum  CandidateStatusEnum
    {
        [Description("New")] New = 1,
        [Description("Recruitment Interview")] RecruitmentInterview = 2,
        [Description("Tech Interview")] TechInterview = 3,
        [Description("Hired")] Hired = 4
    }
}
