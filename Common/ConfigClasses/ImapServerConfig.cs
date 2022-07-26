using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConfigClasses
{
    public class ImapServerConfig
    {
        public string Imap { get; set; }
        public int ImapPort { get; set; }
        public string MailBoxLogin { get; set; }
        public string MailBoxPassword { get; set; }
    }
}