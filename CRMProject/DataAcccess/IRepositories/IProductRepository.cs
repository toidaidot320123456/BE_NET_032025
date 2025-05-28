using DataAcccess.DBContext;

namespace DataAcccess.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product, int>
    {
        void UpdateStockQuantity(int productId, double count);
        bool CheckProductStock(int productId, double count);
        double GetPriceOfOrderDetail(int productId, double count);
    }
}
