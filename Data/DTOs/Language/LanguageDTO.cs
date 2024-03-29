﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Language
{
    public class LanguageDTO
    {
        public LanguageDTO(string name, string language)
        {
            Name = name;
            Language = language;
        }
        public string Name { get; set; }
        public string Language { get; set; }
    }
}
