using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleNames
    {
        [Description("Anonymous")] ANONYMOUS = 1,
        [Description("HR Manager")] HR_MANAGER = 2,
        [Description("Recruiter")] RECRUITER = 3,
        [Description("Technician")] TECHNICIAN = 4,
        [Description("Admin")] ADMIN = 5,
    }
}

//wyciąganie opisu z enum'a: https://www.dotnetstuffs.com/string-enum-values-with-spaces-description-attribute-csharp/