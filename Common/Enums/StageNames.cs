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
    public enum StageNames
    {
        [Description("Evaluation")] EVALUATION = 1,
        [Description("Interview")] INTERVIEW = 2,
        [Description("Phone Interview")] PHONE_INTERVIEW = 3,
        [Description("Tech Interview")] TECH_INTERVIEW = 4,
        [Description("Offer")] OFFER = 5
    }
}
