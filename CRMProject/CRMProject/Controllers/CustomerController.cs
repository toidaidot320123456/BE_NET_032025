using AutoMapper;
using CRMProject.Filter;
using DataAcccess.Common;
using DataAcccess.DBContext;
using DataAcccess.DTO;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using DataAcccess.ResponseData;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;
using System.Security.Claims;

namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] bỏ ApiController, mất đi nhiều tính năng "tự động"
    //nên cần cẩn thận hơn
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public readonly IMapper _mapper;
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [CustomAuthorize("Customer_GetCustomers", "IsView")]
        [HttpPost]
        [Route("GetCustomers")]
        public async Task<ActionResult> GetCustomers()
        {
            List<Customer> customers = await _customerService.GetAll();
            var customerDTOs = _mapper.Map<List<CustomerDTO>>(customers);
            ResponseList<CustomerDTO, int> response = new ResponseList<CustomerDTO, int>(true, MessageResponse.SuccessAction, StatusResponse.Success, customerDTOs, customerDTOs.Count);
            return Ok(response);
        }

        [CustomAuthorize("Customer_Insert", "IsInsert")]
        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> Insert([FromBody] CreateCustomer customer)
        {
            if (!Sercurity.CheckSpecicalCharacter(customer.CustomerName)
                || !Sercurity.CheckXSSInput(customer.CustomerName))
                return BadRequest(MessageResponse.FailureAction);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _customerService.Insert(customer);
                Response<int> response = new Response<int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
                return Ok(response);
            }
        }

        [CustomAuthorize("Customer_Update", "IsUpdate")]
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] EditCustomer customer)
        {
            if (!Sercurity.CheckSpecicalCharacter(customer.CustomerName)
                || !Sercurity.CheckXSSInput(customer.CustomerName))
                return BadRequest(MessageResponse.FailureAction);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _customerService.Update(customer);
                Response<int> response = new Response<int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result);
                return Ok(response);
            }
        }

        [CustomAuthorize("Customer_Remove", "IsDelete")]
        [HttpGet]
        [Route("Remove")]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            var result = await _customerService.Remove(id);
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
