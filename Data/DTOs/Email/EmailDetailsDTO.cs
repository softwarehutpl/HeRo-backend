using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Email
{
    public class EmailDetailsDTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Body { get; set; }
    }
}