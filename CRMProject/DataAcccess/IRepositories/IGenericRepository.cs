using DataAcccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DataAcccess.IRepositories
{
    public interface IGenericRepository<T, IdT> where T : class
    {
        Task<List<T>> GetAll();
        Task<int> Insert(T t);
        Task<int> Update(T t);
        Task<int> Remove(T t);
        T GetById(IdT id);
    }
}
