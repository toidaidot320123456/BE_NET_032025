namespace DataAcccess.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime ExpriedTime { get; set; }
        public string? RefreshToken { get; set; }
    }
}
