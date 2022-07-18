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
        [Description("Evaluation")] EVALUATION,
        [Description("Interview")] INTERVIEW,
        [Description("Phone Interview")] PHONE_INTERVIEW,
        [Description("Tech Interview")] TECH_INTERVIEW,
        [Description("Offer")] OFFER
    }
}
