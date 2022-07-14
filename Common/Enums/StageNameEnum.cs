using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum StageNameEnum
    {
        [Description("Evaluation")] Evaluation = 1,
        [Description("Interview")] Interview = 2,
        [Description("Phone Interview")] PhoneInterview = 3,
        [Description("Tech Interview")] TechInterview = 4,
        [Description("Offer")] Offer = 5
    }
}
