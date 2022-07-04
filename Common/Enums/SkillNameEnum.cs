using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum SkillNameEnum
    {
        [Description("C#")] C_Sharp = 1,
        [Description("C++")] C_PlusPlus = 2,
        [Description("Java")] Java = 3,
        [Description("JavaScript")] JavaScript = 4,
        [Description("React.JS")] React = 5,
        [Description("Angular.JS")] Angular = 6,
        [Description("Vue.JS")] Vue = 7,
        [Description("ASP.NET")] DotNET = 8,
        [Description("HTML")] HTML = 9,
        [Description("CSS")] CSS = 10,
        [Description("SQL")] SQL = 11,
        [Description("Git")] Git = 12      
    }
}
//wyciąganie opisu z enum'a: https://www.dotnetstuffs.com/string-enum-values-with-spaces-description-attribute-csharp/