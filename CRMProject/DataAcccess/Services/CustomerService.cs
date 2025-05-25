using DataAcccess.DBContext;
using DataAcccess.IRepositories;
using DataAcccess.IServices;
using DataAcccess.RequestData;

namespace DataAcccess.Services
{
    public class CustomerService : GenericService<Customer, int>, ICustomerService
    {
        private ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _genericRepository = customerRepository;
        }
        public async Task<int> Insert(CreateCustomer createCustomer)
        {
            var customer = new Customer()
            {
                CustomerName = createCustomer.CustomerName,
                ContactNumber = createCustomer.ContactNumber,
                Email = createCustomer.Email,
                Address = createCustomer.Address
            };
            _customerRepository.Insert(customer);
            return await _customerRepository.SaveChanges();
        }
        public async Task<int> Update(EditCustomer editCustomer)
        {
            var customer = new Customer()
            {
                CustomerId = editCustomer.CustomerId,
                CustomerName = editCustomer.CustomerName,
                ContactNumber = editCustomer.ContactNumber,
                Email = editCustomer.Email,
                Address = editCustomer.Address
            };
            _customerRepository.Update(customer);
            return await _customerRepository.SaveChanges();
        }
        public async Task<int> Remove(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer != null)
            {
                _customerRepository.Remove(customer);
                return await _customerRepository.SaveChanges();
            }
            return await Task.FromResult(0);
        }
    }
}
