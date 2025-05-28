using DataAcccess.DBContext;

namespace DataAcccess.IRepositories
{
    public interface IOrderDetailRepository : IGenericRepository<OrdersDetail, int>
    {
        void DeleteByOrderId(int orderId);
    }
}
