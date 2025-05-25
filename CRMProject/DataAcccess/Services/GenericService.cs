using DataAcccess.IRepositories;
using DataAcccess.IServices;

namespace DataAcccess.Services
{
    public abstract class GenericService<T, IdT> : IGenericService<T, IdT> where T : class
    {
        protected IGenericRepository<T, IdT> _genericRepository;
        public async Task<List<T>> GetAll()
        {
            return await _genericRepository.GetAll();
        }
        public T GetById(IdT id)
        {
            return _genericRepository.GetById(id);
        }

        //public async Task<int> Insert(T t)
        //{
        //    _genericRepository.Insert(t);
        //    return await _genericRepository.SaveChanges();
        //}

        //public async Task<int> Remove(T t)
        //{
        //    _genericRepository.Remove(t);
        //    return await _genericRepository.SaveChanges();
        //}
        //public async Task<int> Update(T t)
        //{
        //    _genericRepository.Update(t);
        //    return await _genericRepository.SaveChanges();
        //}
    }
}
