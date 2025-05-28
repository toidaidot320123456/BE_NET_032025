namespace DataAcccess.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? TotalAmount { get; set; }
        public List<OrderDetailDTO> orderDetails { get; set; }
    }
}
