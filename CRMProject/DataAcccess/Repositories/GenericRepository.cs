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
        public T Insert(T entity)
        {
            _context.Add(entity);
            return entity;
        }
        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
        public void Remove(T t)
        {
            _context.Remove(t);
        }

        public T GetById(IdT id)
        {
            return _set.Find(id);
        }
        public async Task<List<T>> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }
        public async Task<int> SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
