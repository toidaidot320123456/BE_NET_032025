using DataAcccess.DBContext;

namespace DataAcccess.IRepositories
{
    public interface IPermissionRepository : IGenericRepository<Permission, int>
    {
        Task<bool> IsPermission(string functionCode, int userId, bool isInsert, bool isUpdate, bool isDelete, bool isView);
    }
}
