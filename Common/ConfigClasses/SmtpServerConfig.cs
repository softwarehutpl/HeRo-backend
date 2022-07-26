using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConfigClasses
{
    public class SmtpServerConfig
    {
        public string Smtp { get; set; }
        public int SmptPort { get; set; }
        public string FullName { get; set; }
        public string MailBoxLogin { get; set; }
        public string MailBoxPassword { get; set; }
    }
}