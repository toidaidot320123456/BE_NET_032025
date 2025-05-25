using System;
using System.Collections.Generic;

namespace DataAcccess.DBContext
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? ExpriedTime { get; set; }
        public string? RefreshToken { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
