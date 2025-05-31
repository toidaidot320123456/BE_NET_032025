using System.ComponentModel.DataAnnotations;

namespace DataAcccess.RequestData
{
    public class EditCustomer
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "CustomerName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "CustomerName must be between 3 and 50 characters")]
        public string CustomerName { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "ContactNumber must be 10 characters")]
        public string? ContactNumber { get; set; }
        [EmailAddress(ErrorMessage = "Email.")]
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
    public class CreateCustomer
    {
        [Required(ErrorMessage = "CustomerName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "CustomerName must be between 3 and 50 characters")]
        public string CustomerName { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "ContactNumber must be 10 characters")]
        public string? ContactNumber { get; set; }
        [EmailAddress(ErrorMessage = "Email.")]
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
