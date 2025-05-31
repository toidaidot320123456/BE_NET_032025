using System.ComponentModel.DataAnnotations;

namespace DataAcccess.RequestData
{
    public class EditProduct
    {
        [Required(ErrorMessage = "ProductId is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "ProductName must be between 3 and 50 characters")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "UnitPrice is required")]
        public double UnitPrice { get; set; }
        public double StockQuantity { get; set; }
    }
    public class CreateProduct
    {
        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "ProductName must be between 3 and 50 characters")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "UnitPrice is required")]
        public double UnitPrice { get; set; }
        public double StockQuantity { get; set; }
    }
}
