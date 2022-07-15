using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum UserStatus
    {
        [Description("Not Verified")] Not_verified = 1,
        [Description("Active")] Active = 2,
        [Description("Archived")] Archived = 3,
        [Description("Deleted")] Deleted = 4
    }
}
