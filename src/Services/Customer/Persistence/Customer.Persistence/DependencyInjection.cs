using Customer.Domain.Interfaces;

using Customer.Persistence.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SentezMicro.Services.Customer.Customer.Persistence.Data;

namespace Customer.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
            //                                    ServiceLifetime.Singleton,
            //                                    ServiceLifetime.Singleton);

            var cnn= configuration.GetConnectionString("CustomerConnection");
            services.AddDbContext<CustomerContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("CustomerConnection"),
                        b => b.MigrationsAssembly(typeof(CustomerContext).Assembly.FullName)), ServiceLifetime.Singleton);

            //Add Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            
            return services;
        }
    }
}
