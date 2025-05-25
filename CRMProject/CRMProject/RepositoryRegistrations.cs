using DataAcccess.IRepositories;
using DataAcccess.Repositories;

namespace CRMProject
{
    public static class RepositoryRegistrations
    {
        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
