using DataAcccess.DBContext;

namespace DataAcccess.IRepositories
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        Task<Order> GetDetail(int Id);
        Task<int> DeleteOrderTEST(int id);
    }
}
