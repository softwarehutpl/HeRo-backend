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
        [Description("HR Manager")] HR_manager = 2,
        [Description("Recruiter")] recruiter = 3,
        [Description("Interviewer")] interviewer = 4,
        [Description("Admin")] admin = 5,
    }
}

//wyciąganie opisu z enum'a: https://www.dotnetstuffs.com/string-enum-values-with-spaces-description-attribute-csharp/