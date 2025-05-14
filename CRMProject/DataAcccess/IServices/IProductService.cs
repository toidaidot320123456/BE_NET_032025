using DataAcccess.DBContext;
using DataAcccess.RequestData;

namespace DataAcccess.IServices
{
    public interface IProductService
    {
        Task<int> Insert(CreateProduct product);
        Task<int> Update(EditProduct product);
        Task<int> Remove(int id);
        Task<List<Product>> GetAll();
    }
}
