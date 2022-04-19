
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Product.Persistence.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase (this IHost host)
        {
            using (var scope=host.Services.CreateScope())
            {
                //try
                {
                    var productContext = scope.ServiceProvider.GetRequiredService<ProductDataContext>();
                    productContext.Database.Migrate();

                    ProductContextSeed.SeedTask(productContext).Wait();

                }
               // catch (Exception)
                {

                  //  throw;
                }
            }

            return host;
                }
    }
}
