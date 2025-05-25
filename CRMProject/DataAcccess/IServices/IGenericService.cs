namespace DataAcccess.IServices
{
    public interface IGenericService<T, IdT>
    {
        Task<List<T>> GetAll();
        T GetById(IdT id);
        //Task<int> Insert(T t);
        //Task<int> Remove(T t);
        //Task<int> Update(T t);
    }
}
