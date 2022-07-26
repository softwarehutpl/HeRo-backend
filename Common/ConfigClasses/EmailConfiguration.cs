using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConfigClasses
{
    public class EmailConfiguration
    {
        public string CompanyEmail { get; set; }
        public string CompanyEmailPassword { get; set; }
        public string SmptGmail { get; set; }
        public int SmptPortGmail { get; set; }
        public string SmptPortOutlook { get; set; }
        public string SmptOutlook { get; set; }
    }
}