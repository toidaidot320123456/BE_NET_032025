using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAcccess.Repositories
{
    public class ProductRepository : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.Products;
        }
    }
}
