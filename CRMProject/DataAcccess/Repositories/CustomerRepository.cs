using DataAcccess.DBContext;
using DataAcccess.IRepositories;

namespace DataAcccess.Repositories
{
    public class CustomerRepository : GenericRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(BE_NET_032025Context context) : base(context)
        {
            this._set = context.Customers;
        }
    }
}
