using DataAcccess.DBContext;
using DataAcccess.IRepositories;

namespace DataAcccess.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrdersDetail, int>, IOrderDetailRepository
    {
        public OrderDetailRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.OrdersDetails;
        }
        public void DeleteByOrderId(int orderId)
        {
            var orderDetails = _context.OrdersDetails.Where(x => x.OrderId == orderId);
            if (orderDetails != null)
            {
                _context.RemoveRange(orderDetails);
            }
        }
    }
}
