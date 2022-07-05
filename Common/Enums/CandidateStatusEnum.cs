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
        [Description("New Application Submitted")] Applying = 1,
        [Description("Application Reviewed")] ApplicationReviewed = 2,
        [Description("Invited for the Recruitment Interview")] RecruitmentInterview = 3,
        [Description("Invited for the Technical Interview")] TechnicalInterview = 4,
        [Description("Application Accepted")] Accepted = 5,
        [Description("Application Rejected")] Rejected = 6
    }
}
