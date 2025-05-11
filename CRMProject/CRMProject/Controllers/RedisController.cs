//using DataAcccess.DBContext;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Distributed;
//using Newtonsoft.Json;
//using System.Text;
//namespace CRMProject.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RedisController : ControllerBase
//    {
//        private readonly IDistributedCache _distributedCache;
//        public RedisController(IDistributedCache distributedCache)
//        {
//            _distributedCache = distributedCache;
//        }

//        [HttpPost]
//        [Route("Add")] //Bắt buộc phải có để phân biệt các Action với nhau
//        public async Task<ActionResult> Add()
//        {
//            //var cacheKey = "keyCache12345";
//            //var data = new Customer() { Address = "123 Main Street, New York", Email = "johndoe@example.com", FullName = "John Doe", PhoneNumber = "1234567890", CustomerId = 1 };
//            ////chuyển data thành byte[]
//            //var dataCacheJson = JsonConvert.SerializeObject(data);
//            //var dataToCache = Encoding.UTF8.GetBytes(dataCacheJson);
//            ////options Cache thời hạn 5 phút
//            //DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
//            //options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
//            ////set cache 
//            //_distributedCache.Set(cacheKey, dataToCache, options);

//            try
//            {
//                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
//                options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
                
//                //chuỗi
//                string chuoi = "Chuỗi dữ liệu";
//                var dataToCache = Encoding.UTF8.GetBytes(chuoi);
//                _distributedCache.Set("chuoi", dataToCache, options);

//                //số nguyên
//                int songuyen = 101;
//                var dataCacheJson = JsonConvert.SerializeObject(songuyen);
//                dataToCache = Encoding.UTF8.GetBytes(dataCacheJson);
//                _distributedCache.Set("songuyen", dataToCache, options);

//                //class tự định nghĩa
//                Student student = new Student() { Id = 1, Name = "Hà Lan"};
//                dataCacheJson = JsonConvert.SerializeObject(student);
//                dataToCache = Encoding.UTF8.GetBytes(dataCacheJson);
//                _distributedCache.Set("class", dataToCache, options);


//                return Ok("Thành công");
//            }
//            catch(Exception ex)
//            {

//            }
//            return Ok("Thất bại");
//        }

//        [HttpPost]
//        [Route("Remove")] //Bắt buộc phải có để phân biệt các Action với nhau
//        public async Task<ActionResult> Remove()
//        {
//            var cacheKey = "keyCache12345";
//            //xóa cache 
//            _distributedCache.Remove(cacheKey);
//            return Ok();
//        }

//        [HttpPost]
//        [Route("Get")] //Bắt buộc phải có để phân biệt các Action với nhau
//        public async Task<ActionResult> Get()
//        {
//            var cacheKey = "keyCache12345";
//            //xóa cache 
//            var bytes = _distributedCache.Get(cacheKey);
//            var json = Encoding.UTF8.GetString(bytes);
//            var data = JsonConvert.SerializeObject(json);
//            return Ok(data);
//        }

//        [HttpPost]
//        [Route("Refresh")] //Bắt buộc phải có để phân biệt các Action với nhau
//        public async Task<ActionResult> Refresh()
//        {
//            //Refresh cache 
//            _distributedCache.Refresh("chuoi");
//            _distributedCache.Refresh("songuyen");
//            _distributedCache.Refresh("class");
//            return Ok("Thành công");
//        }


//    }
//}
