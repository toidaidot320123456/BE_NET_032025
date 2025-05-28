using DataAcccess.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAcccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChange();
        void Dispose();
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IOrderRepository _orderRepository { get;}
        IOrderDetailRepository _orderDetailRepository { get;}
        IProductRepository _productRepository { get;}
    }
}
