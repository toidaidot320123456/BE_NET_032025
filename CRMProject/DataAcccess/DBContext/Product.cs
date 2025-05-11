using System;
using System.Collections.Generic;

namespace DataAcccess.DBContext
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public string? ImagePath { get; set; }
    }
}
