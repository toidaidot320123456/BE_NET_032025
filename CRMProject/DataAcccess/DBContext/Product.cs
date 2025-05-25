using System;
using System.Collections.Generic;

namespace DataAcccess.DBContext
{
    public partial class Product
    {
        public Product()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public double? UnitPrice { get; set; }
        public double? StockQuantity { get; set; }

        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
