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
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    //[ApiController] bỏ ApiController, mất đi nhiều tính năng "tự động"
    //nên cần cẩn thận hơn
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        public CustomerController(ICustomerService customerService, IMapper mapper, IDistributedCache distributedCache)
        {
            _customerService = customerService;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        [CustomAuthorize("Customer_GetCustomers", "IsView")]
        [HttpPost]
        [Route("GetCustomers")]
        public async Task<ActionResult> GetCustomers()
        {
            var keyCache = "Customer_GetCustomers";
            var cachedData = _distributedCache.GetString(keyCache);
            List<CustomerDTO> result;
            if (!string.IsNullOrEmpty(cachedData))
            {
                result = JsonConvert.DeserializeObject<List<CustomerDTO>>(cachedData);
            }
            else
            {
                List<Customer> customers = await _customerService.GetAll();
                result = _mapper.Map<List<CustomerDTO>>(customers);
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };
                _distributedCache.SetString(keyCache, JsonConvert.SerializeObject(result), cacheOptions);
            }
            ResponseList<CustomerDTO, int> response = new ResponseList<CustomerDTO, int>(true, MessageResponse.SuccessAction, StatusResponse.Success, result, result.Count);
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
