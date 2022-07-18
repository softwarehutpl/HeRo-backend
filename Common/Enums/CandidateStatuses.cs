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
        [Description("New")] NEW,
        [Description("In processing")] IN_PROCESSING,
        [Description("Dropped Out")] DROPPED_OUT,
        [Description("Hired")] HIRED,
    }
}
