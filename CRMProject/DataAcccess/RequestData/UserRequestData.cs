using System.ComponentModel.DataAnnotations;

namespace DataAcccess.RequestData
{
    public class LoginRequestData
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        //
        public string? DeviceID { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? BrowserName { get; set; }
        public string? Location { get; set; }
    }
    public class TokenRequestData
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
        [Required(ErrorMessage = "RefreshToken is required")]
        public string RefreshToken { get; set; }
    }
}
