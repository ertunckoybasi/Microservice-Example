
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SentezMicro.Services.Customer.Customer.Persistence.Data;
using SentezMicro.Services.Customer.Persistence.Data;

namespace Customer.Api.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase (this IHost host)
        {
            using (var scope=host.Services.CreateScope())
            {
                //try
                {
                    var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                    //var customerContextSeed = scope.ServiceProvider.GetRequiredService<CustomerContexSeed>();
                    customerContext.Database.Migrate();

                    CustomerContexSeed.SeedTask(customerContext).Wait();

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
