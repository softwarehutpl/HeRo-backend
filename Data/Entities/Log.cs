﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Log")]
    public class Log
    {
        [Key]
        public int Id { get; set; }

        public DateTime Datetime { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public string MachineName { get; set; }
        public string Logger { get; set; }
    }
}