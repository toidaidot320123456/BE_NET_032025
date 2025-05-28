using DataAcccess.DBContext;
using DataAcccess.IRepositories;

namespace DataAcccess.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.Products;
        }
        public void UpdateStockQuantity(int productId, double count)
        {
            var product = _context.Products.Find(productId);
            if (product != null) {
                product.StockQuantity -= count;
            }
        }

        public bool CheckProductStock(int productId, double count)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                if (product.StockQuantity < count)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        
        public double GetPriceOfOrderDetail(int productId, double count)
        {
            var product = _context.Products.Find(productId);
            if (product != null && product.UnitPrice.HasValue)
            {
                return product.UnitPrice.Value * count;
            }
            return 0;
        }

    }
}
