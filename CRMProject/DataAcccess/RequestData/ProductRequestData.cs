using System.ComponentModel.DataAnnotations;

namespace DataAcccess.RequestData
{
    public class EditProduct
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "ProductName must be between 3 and 50 characters")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        public string ImagePath { get; set; }
    }
    public class CreateProduct
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "ProductName must be between 3 and 50 characters")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        public string ImagePath { get; set; }
    }
}
