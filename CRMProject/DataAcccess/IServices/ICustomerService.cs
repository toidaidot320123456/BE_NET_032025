using DataAcccess.DBContext;
using DataAcccess.RequestData;

namespace DataAcccess.IServices
{
    public interface ICustomerService: IGenericService<Customer, int>
    {
        Task<int> Insert(CreateCustomer createCustomer);
        Task<int> Update(EditCustomer editCustomer);
        Task<int> Remove(int id);
    }
}
