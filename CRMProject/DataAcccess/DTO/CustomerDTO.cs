using DataAcccess.DBContext;

namespace DataAcccess.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
