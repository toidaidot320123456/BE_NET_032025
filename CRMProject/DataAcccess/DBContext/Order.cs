using System;
using System.Collections.Generic;

namespace DataAcccess.DBContext
{
    public partial class Order
    {
        public Order()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? TotalAmount { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
