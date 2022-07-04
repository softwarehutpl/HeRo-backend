using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum UserStatusEnum
    {
        [Description("Not Verified")] not_verified = 1,
        [Description("Active")] active = 2,
        [Description("Archived")] archived = 3,
        [Description("Deleted")] deleted = 4
    }
}
