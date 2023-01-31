﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Item
{
    public class Item
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Comments { get; set; }
    }
}
