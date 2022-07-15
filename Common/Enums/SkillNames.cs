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
    public enum SkillNames
    {
        [Description("C#")] C_SHARP = 1,
        [Description("C++")] C_PLUSPLUS = 2,
        [Description("Java")] JAVA = 3,
        [Description("JavaScript")] JAVASCRIPT = 4,
        [Description("React.JS")] REACT = 5,
        [Description("Angular.JS")] ANGULAR = 6,
        [Description("Vue.JS")] VUE = 7,
        [Description("ASP.NET")] DOTNET = 8,
        [Description("HTML")] HTML = 9,
        [Description("CSS")] CSS = 10,
        [Description("SQL")] SQL = 11,
        [Description("Git")] GIT = 12      
    }
}
//wyciąganie opisu z enum'a: https://www.dotnetstuffs.com/string-enum-values-with-spaces-description-attribute-csharp/