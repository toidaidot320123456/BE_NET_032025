using DataAcccess.DBContext;
using DataAcccess.IServices;

namespace DataAcccess.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public BE_NET_032025Context _context { get; set; }
        public GenericRepository(BE_NET_032025Context context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return _context.Set<T>().ToList();
        }
    }
}
