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
    public enum UserStatuses
    {
        [Description("Not Verified")] NOT_VERIFIED= 1,
        [Description("Active")] ACTIVE = 2,
        [Description("Archived")] ARCHIVED = 3,
        [Description("Deleted")] DELETED = 4
    }
}
