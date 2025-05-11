using DataAcccess.DBContext;

namespace DataAcccess.IServices
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        BE_NET_032025Context _context { get; set; }
    }
}
