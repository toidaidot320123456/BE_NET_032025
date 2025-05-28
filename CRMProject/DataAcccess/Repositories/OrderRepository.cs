using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAcccess.Repositories
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.Orders;
        }
        public async Task<Order> GetDetail(int Id)
        {
            var query = _context.Orders.Where(x => x.OrderId == Id);
            query = query.Include(x => x.OrdersDetails);
            var result = query.FirstOrDefault();
            return result;
        }
        public async Task<int> DeleteOrderTEST(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Entry(order).Collection(x=>x.OrdersDetails).Load();
                _context.Remove(order);
                return _context.SaveChanges();
            }
            return await Task.FromResult(0);
        }
    }
}
