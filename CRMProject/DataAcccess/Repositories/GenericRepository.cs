using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace DataAcccess.Repositories
{
    public class GenericRepository<T, IdT> : IGenericRepository<T, IdT> where T : class
    {
        protected BE_NET_032025Context _context { get; set; }
        protected DbSet<T> _set { get; set; }
        public GenericRepository(BE_NET_032025Context context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<int> Insert(T t)
        {
            _context.Add(t);
            return _context.SaveChanges();
        }
        public async Task<int> Update(T t)
        {
            _context.Update(t);
            return _context.SaveChanges();
        }
        public async Task<int> Remove(T t)
        {
            _context.Remove(t);
            return _context.SaveChanges();
        }
        public T GetById(IdT id)
        {
            return _set.Find(id);
        }
    }
}
