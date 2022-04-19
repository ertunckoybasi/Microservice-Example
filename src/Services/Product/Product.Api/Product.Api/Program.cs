using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Product.Persistence.Data;

namespace Product.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().
                MigrateDatabase().
                Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
