using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using DataAcccess.IServices;
using DataAcccess.RequestData;

namespace DataAcccess.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<int> Insert(CreateProduct productRequest)
        {
            var product = new Product()
            {
                ProductName = productRequest.ProductName,
                ImagePath = productRequest.ImagePath,
                Price = productRequest.Price
            };
            return _productRepository.Insert(product);
        }
        public Task<int> Update(EditProduct productRequest)
        {
            var product = new Product()
            {
                ProductName = productRequest.ProductName,
                ImagePath = productRequest.ImagePath,
                Price = productRequest.Price,
                Id = productRequest.Id
            };
            return _productRepository.Update(product);
        }
        public Task<int> Remove(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {               
                return _productRepository.Remove(product);
            }
            return Task.FromResult(0);
        }
        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }
    }
}
