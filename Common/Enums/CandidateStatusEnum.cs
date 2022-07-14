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
        [Description("In processing")] InProcessing = 2,
        [Description("Dropped Out")] DroppedOut = 3,
        [Description("Hired")] Hired =4,
    }
}
