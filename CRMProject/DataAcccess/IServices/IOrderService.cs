using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.RequestData;

namespace DataAcccess.IServices
{
    public interface IOrderService : IGenericService<Order, int>
    {
        Task<int> Insert(CreateOrder createOrder);
        Task<bool> IsProductInStock(CreateOrder createOrder);
        Task<OrderDTO> GetDetail(int Id);
        Task<int> DeleteOrderTEST(int id);
        Task<int> DeleteOrder(int id);
    }
}
