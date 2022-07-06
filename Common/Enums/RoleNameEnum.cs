using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum RoleNameEnum
    {
        [Description("Anonymous")] Anonymous = 1,
        [Description("HR Manager")] HRmanager = 2,
        [Description("Recruiter")] Recruiter = 3,
        [Description("Interviewer")] Interviewer = 4,
        [Description("Admin")] Admin = 5,
    }
}

//wyciąganie opisu z enum'a: https://www.dotnetstuffs.com/string-enum-values-with-spaces-description-attribute-csharp/