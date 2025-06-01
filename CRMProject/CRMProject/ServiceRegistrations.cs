using CRMProject.Cache;
using DataAcccess.IServices;
using DataAcccess.Services;

namespace CRMProject
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServiceRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }
    }
}
