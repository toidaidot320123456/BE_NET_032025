using DataAcccess.DBContext;
using DataAcccess.IRepositories;

namespace DataAcccess.Repositories
{
    public class PermissionRepository : GenericRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.Permissions;
        }
        public async Task<bool> IsPermission(string functionCode, int userId, bool isInsert, bool isUpdate, bool isDelete, bool isView)
        {
            //Cách 2
            var result = (from p in _context.Permissions
                         join f in _context.Functions
                         on p.FunctionId equals f.FunctionId
                         where (p.UserId == userId && f.FunctionCode == functionCode)
                         && ((p.IsView == true && isView == true) || (isView == false))
                         && (p.IsInsert == true && isInsert == true || (isInsert == false))
                         && (p.IsUpdate == true && isUpdate == true || (isUpdate == false))
                         && (p.IsDelete == true && isDelete == true || (isDelete == false))
                         select p).FirstOrDefault();
            return result != null ? true : false;

            //Cách 1
            //var function = _context.Functions.Where(x => x.FunctionCode == functionCode).FirstOrDefault();
            //if (function != null)
            //{
            //    _context.Entry(function).Collection(x => x.Permissions).Load();
            //    var query = function.Permissions.Where(x => x.FunctionId == function.FunctionId && x.UserId == userId);
            //    if (isInsert)
            //    {
            //        query = query.Where(x => x.IsInsert == true);
            //    }
            //    if (isUpdate)
            //    {
            //        query = query.Where(x => x.IsUpdate == true);
            //    }
            //    if (isDelete)
            //    {
            //        query = query.Where(x => x.IsDelete == true);
            //    }
            //    if (isView)
            //    {
            //        query = query.Where(x => x.IsView == true);
            //    }
            //    var result = query.FirstOrDefault();
            //    return result != null ? true : false;
            //}
            //return await Task.FromResult(false);
        }
    }
}
