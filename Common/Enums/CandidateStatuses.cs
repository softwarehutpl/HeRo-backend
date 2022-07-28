using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CandidateStatuses
    {
        [Description("New")] NEW,
        [Description("In processing")] IN_PROCESSING,
        [Description("Dropped Out")] DROPPED_OUT,
        [Description("Hired")] HIRED,
    }
}