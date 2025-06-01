using CRMProject.Cache;
using DataAcccess.DTO;
using DataAcccess.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CRMProject.Filter
{
    //Attribute
    //Bắt buộc tên class kết thúc là Attribute
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string _functionCode, string _permission) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { _functionCode, _permission };
            //chú ý thứ tự các Arguments và kiểu dữ liệu
        }
    }
    //Attribute có một cái Filter
    public class AuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _functionCode;
        private readonly string _permission;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ICacheService _cacheService;
        public AuthorizeActionFilter(string functionCode, string permission, IPermissionRepository permissionRepository, ICacheService cacheService)
        //chú ý thứ tự các tham số và kiểu dữ liệu để nhận được giá trị từ Arguments truyền vào
        {
            _functionCode = functionCode;
            _permission = permission;
            _permissionRepository = permissionRepository;
            _cacheService = cacheService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            //bắt buộc sử dụng app.UseAuthentication(); để tự động gán token cho HttpContext.User

            if (identity == null || !identity.IsAuthenticated)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.ContentType = "application/json";
                context.Result = new JsonResult(new { message = "Vui lòng đăng nhập!" });
                return;
            }
            var claims = identity.Claims;
            var userId = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value);
            var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var isAdmin = claims.FirstOrDefault(x => x.Type == ClaimTypes.IsPersistent)?.Value;
            var deviceName = claims.FirstOrDefault(x => x.Type == ClaimTypes.WindowsDeviceClaim)?.Value;

            //kiểm tra token trong Redis
            var keyCache = string.Format("UserSession_{0}_{1}", deviceName, userName);
            var userSession = await _cacheService.GetAsync<UserSessionDTO>(keyCache);
            if (userSession == null)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.ContentType = "application/json";
                context.Result = new JsonResult(new { message = "Thiết bị đã đăng xuất!" });
                return;
            }

            if (isAdmin != "True")
            {
                var flag = await IsPermission(_functionCode, _permission, userId);
                if (flag == false)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.HttpContext.Response.ContentType = "application/json";
                    context.Result = new JsonResult(new { message = "Bạn không có quyền!" });
                    return;
                }
            }
        }

        public async Task<bool> IsPermission(string functionCode, string _permission, int userId)
        {
            switch (_permission)
            {
                case "IsInsert":
                    return await _permissionRepository.IsPermission(_functionCode, userId, true, false, false, false);
                case "IsUpdate":
                    return await _permissionRepository.IsPermission(_functionCode, userId, false, true, false, false);
                case "IsDelete":
                    return await _permissionRepository.IsPermission(_functionCode, userId, false, false, true, false);
                case "IsView":
                    return await _permissionRepository.IsPermission(_functionCode, userId, false, false, false, true);
                default:
                    return false;
            }
        }

    }

}
