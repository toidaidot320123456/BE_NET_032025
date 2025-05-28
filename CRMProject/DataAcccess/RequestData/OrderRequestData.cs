using System.ComponentModel.DataAnnotations;

namespace DataAcccess.RequestData
{
    public class CreateOrder
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "OrderDate is required")]
        public DateTime OrderDate { get; set; }
        public List<CreateOrderDetail> OrderDetails { get; set; }
    }
    public class CreateOrderDetail
    {
        [Required(ErrorMessage = "ProductId is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public double Quantity { get; set; }
    }
}
