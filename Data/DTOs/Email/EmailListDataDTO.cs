using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Email
{
    public class EmailListDataDTO
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}