using System;
using System.Collections.Generic;

namespace DataAcccess.DBContext
{
    public partial class Permission
    {
        public int PermissionId { get; set; }
        public int? FunctionId { get; set; }
        public int? UserId { get; set; }
        public bool? IsInsert { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsView { get; set; }

        public virtual Function? Function { get; set; }
        public virtual User? User { get; set; }
    }
}
