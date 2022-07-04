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
        [Description("New Application Submitted")] applying = 1,
        [Description("Application Reviewed")] application_reviewed = 2,
        [Description("Invited for the Recruitment Interview")] recruitment_interview = 3,
        [Description("Invited for the Technical Interview")] technical_interview = 4,
        [Description("Application Accepted")] accepted = 5,
        [Description("Application Rejected")] rejected = 6
    }
}
