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
        [Description("New")] New,
        [Description("Interview")] Interview,
        [Description("Technical Interview")] TechnicalInterview,
        [Description("Hired")] Hired
    }
}
