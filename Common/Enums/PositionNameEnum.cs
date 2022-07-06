using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum PositionNameEnum
    {
        [Description("Developer")] Dev = 1,
        [Description("Designer")] Designer = 2,
        [Description("Tester")] Tester = 3,
        [Description("HR")] HR = 4
    }
}
