﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Nr { get; set; }
    }
}
