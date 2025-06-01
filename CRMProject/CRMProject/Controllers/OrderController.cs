using AutoMapper;
using CRMProject.Cache;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using DataAcccess.ResponseData;
using Microsoft.AspNetCore.Mvc;
namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public OrderController(IOrderService orderService, IMapper mapper, ICacheService cacheService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpPost]
        [Route("GetOrders")]
        public async Task<ActionResult> GetOrders()
        {
            var keyCache = "Order_GetOrders";
            List<Order> orders;
            var cachedData =  await _cacheService.GetAsync<List<Order>>(keyCache);
            if (cachedData != null)
            {
                orders = cachedData;
            }
            else
            {
                orders = await _orderService.GetAll();
                await _cacheService.SetAsync<List<Order>>(keyCache, orders, TimeSpan.FromMinutes(2));
            }
            ResponseList<Order, int> response = new ResponseList<Order, int>(true, MessageResponse.SuccessAction, StatusResponse.Success, orders, orders.Count);
            return Ok(response);
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Insert([FromBody] CreateOrder order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {

                if (await _orderService.IsProductInStock(order) == false)
                    return BadRequest(MessageResponse.NotEnoughStock);

                var result = await _orderService.Insert(order);
                if (result < 0)
                {
                    Response response = new Response(true, MessageResponse.SuccessAction, StatusResponse.Success);
                    return Ok(response);
                }
                else
                {
                    Response<int> response = new Response<int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
                    return Ok(response);
                }
            }
        }

        [HttpPost]
        [Route("GetDetail/{Id}")]
        public async Task<ActionResult> GetDetail([FromRoute] int Id)
        {
            var result = await _orderService.GetDetail(Id);
            Response<OrderDTO> response = new Response<OrderDTO>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
            if (result == null)
            {
                response.Success = false;
                response.Message = MessageResponse.FailureAction;
                response.Status = StatusResponse.Failure;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Remove/{Id}")]
        public async Task<ActionResult> Remove([FromRoute] int Id)
        {
            var result = await _orderService.DeleteOrder(Id);
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
