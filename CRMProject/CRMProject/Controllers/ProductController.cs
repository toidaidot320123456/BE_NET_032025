using AutoMapper;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] bỏ ApiController, mất đi nhiều tính năng "tự động"
    //nên cần cẩn thận hơn
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            List<Product> products = await _productService.GetAll();
            var productDTOs = _mapper.Map<List<ProductDTO>>(products);
            return Ok(productDTOs);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Insert([FromBody] CreateProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _productService.Insert(product);
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] EditProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            else
            {
                var result = await _productService.Update(product);
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            var result = await _productService.Remove(id);
            return Ok(result);
        }

    }
}
