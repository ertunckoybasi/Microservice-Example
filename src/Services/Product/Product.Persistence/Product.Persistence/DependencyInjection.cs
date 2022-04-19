

using Customer.Persistence.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Application.Interfaces;
using Product.Persistence.Data;
using Product.Persistence.Repositories.Dapper;

namespace Product.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
            //                                    ServiceLifetime.Singleton,
            //                                    ServiceLifetime.Singleton);

            // string cnnStr = configuration.GetConnectionString("ProductConnection");
            services.AddDbContext<ProductDataContext>(options =>
           options.UseNpgsql(configuration.GetConnectionString("PostgreConnection")));

            //Add Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IProductRepository, ProductRepository>();

            
            return services;
        }
    }
}
