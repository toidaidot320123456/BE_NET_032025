namespace DataAcccess.DTO
{
    public class UserSessionDTO
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string? DeviceID { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string? BrowserName { get; set; }
        public string? Location { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
