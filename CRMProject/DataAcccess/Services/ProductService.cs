using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using DataAcccess.IServices;
using DataAcccess.RequestData;

namespace DataAcccess.Services
{
    public class ProductService : GenericService<Product, int>, IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> Insert(CreateProduct productRequest)
        {
            var product = new Product()
            {
                ProductName = productRequest.ProductName,
                UnitPrice = productRequest.UnitPrice,
                StockQuantity = productRequest.StockQuantity
            };
            _productRepository.Insert(product);
            return await _productRepository.SaveChanges();
        }
        public async Task<int> Update(EditProduct productRequest)
        {
            var product = new Product()
            {
                ProductName = productRequest.ProductName,
                UnitPrice = productRequest.UnitPrice,
                StockQuantity = productRequest.StockQuantity,
                ProductId = productRequest.ProductId
            };
            _productRepository.Update(product);
            return await _productRepository.SaveChanges();
        }
        public async Task<int> Remove(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Remove(product);
                return await _productRepository.SaveChanges();
            }
            return await Task.FromResult(0);
        }
    }
}
