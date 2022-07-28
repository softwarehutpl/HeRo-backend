using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Email
{
    public class EmailDetailsDTO
    {
        public string MessageId { get; set; }
        public string? Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string? Body { get; set; }
        public string? HtmlBody { get; set; }
        public string? Bcc { get; set; }
        public string? Cc { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string? InReplyTo { get; set; }
    }
}