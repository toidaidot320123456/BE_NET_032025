using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAcccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BE_NET_032025Context _context;
        public IOrderRepository _orderRepository { get; set; }
        public IOrderDetailRepository _orderDetailRepository { get; set; }
        public IProductRepository _productRepository { get; set; }
        public UnitOfWork(BE_NET_032025Context context, IOrderRepository orderRepository, 
            IProductRepository productRepository
            , IOrderDetailRepository orderDetailRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
