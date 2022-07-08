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
        public string Smpt { get; set; }
        public int Port { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
