using AutoMapper;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using DataAcccess.ResponseData;
using Microsoft.AspNetCore.Mvc;
namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetOrders")]
        public async Task<ActionResult> GetOrders()
        {
            List<Order> orders = await _orderService.GetAll();
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
