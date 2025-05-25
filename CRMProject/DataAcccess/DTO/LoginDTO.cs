namespace DataAcccess.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
