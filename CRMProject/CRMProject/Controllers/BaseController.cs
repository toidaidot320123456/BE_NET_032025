using DataAcccess.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRMProject.Controllers
{
    public class BaseController : ControllerBase
    {
        //Bắt buộc phải khai báo protected
        protected UserDTO getLoggedInUser()
        {        
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                var claims = HttpContext.User.Claims;
                if (claims != null && claims.Count() > 0)
                {
                    var userId = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value);
                    var userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                    var isAdmin = claims.FirstOrDefault(x => x.Type == ClaimTypes.IsPersistent)?.Value == "True"? true: false;
                    return new UserDTO() { Id = userId, UserName = userName, IsAdmin = isAdmin};
                }
            }
            return null;
        }
    }
}
