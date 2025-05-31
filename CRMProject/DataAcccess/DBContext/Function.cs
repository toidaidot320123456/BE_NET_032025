using System;
using System.Collections.Generic;

namespace DataAcccess.DBContext
{
    public partial class Function
    {
        public Function()
        {
            Permissions = new HashSet<Permission>();
        }

        public int FunctionId { get; set; }
        public string? FunctionName { get; set; }
        public string? FunctionCode { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
