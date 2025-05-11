using DataAcccess.DBContext;
using DataAcccess.IServices;

namespace DataAcccess.Services
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(BE_NET_032025Context context) : base(context)
        {
        }
    }
}
