﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
   public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountPrice { get; set; }

        public int Quantitiy { get; set; }               

        public bool Status { get; set; }

        public int BrandId { get; set; }
    }
}
