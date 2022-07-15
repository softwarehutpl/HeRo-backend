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
    public enum PositionNames
    {
        [Description("Developer")] DEV = 1,
        [Description("Designer")] DESIGNER = 2,
        [Description("Tester")] TESTER = 3,
        [Description("HR")] HR = 4
    }
}
