using AutoMapper;
using CRMProject.Filter;
using DataAcccess.Common;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using DataAcccess.ResponseData;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] bỏ ApiController, mất đi nhiều tính năng "tự động"
    //nên cần cẩn thận hơn
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [CustomAuthorize("Product_GetProducts", "IsView")]
        [HttpPost]
        [Route("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            List<Product> products = await _productService.GetAll();
            var productDTOs = _mapper.Map<List<ProductDTO>>(products);
            ResponseList<ProductDTO, int> response = new ResponseList<ProductDTO, int>(true, MessageResponse.SuccessAction, StatusResponse.Success, productDTOs, productDTOs.Count);
            return Ok(response);
        }

        [CustomAuthorize("Product_Insert", "IsInsert")]
        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Insert([FromBody] CreateProduct product)
        {
            if (!Sercurity.CheckSpecicalCharacter(product.ProductName)
                || !Sercurity.CheckXSSInput(product.ProductName))
                return BadRequest(MessageResponse.FailureAction);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _productService.Insert(product);
                Response<int> response = new Response<int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
                return Ok(response);
            }
        }

        [CustomAuthorize("Product_Update", "IsUpdate")]
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] EditProduct product)
        {
            if (!Sercurity.CheckSpecicalCharacter(product.ProductName)
                || !Sercurity.CheckXSSInput(product.ProductName))
                return BadRequest(MessageResponse.FailureAction);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _productService.Update(product);
                Response<int> response = new Response<int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
                return Ok(response);
            }
        }

        [CustomAuthorize("Product_Remove", "IsDelete")]
        [HttpGet]
        [Route("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            var result = await _productService.Remove(id);
            Response<int> response = new Response<int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
            if (result == 0)
            {
                response.Success = false;
                response.Message = MessageResponse.FailureAction;
                response.Status = StatusResponse.Failure;
            }
            return Ok(response);
        }

    }
}
