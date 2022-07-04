using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum RecruitmentStatusEnum
    {
        [Description("Open for submissions")] open = 1,
        [Description("Closed for submissions")] closed = 2
    }
}
