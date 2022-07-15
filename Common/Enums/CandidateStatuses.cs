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
    public enum  CandidateStatuses
    {
        [Description("New")] NEW = 1,
        [Description("In processing")] IN_PROCESSING = 2,
        [Description("Dropped Out")] DROPPED_OUT = 3,
        [Description("Hired")] HIRED = 4,
    }
}
