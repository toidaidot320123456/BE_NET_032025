namespace DataAcccess.IRepositories
{
    public interface IGenericRepository<T, IdT> where T : class
    {
        T Insert(T entity);
        T Update(T entity);
        void Remove(T t);
        T GetById(IdT id);
        Task<List<T>> GetAll();
        Task<int> SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
