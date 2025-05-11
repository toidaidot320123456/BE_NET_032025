using AutoMapper;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            List<Product> products = await _productRepository.GetAll();
            var productDTOs = _mapper.Map<List<ProductDTO>>(products);
            return Ok(productDTOs);
        }
    }
}
