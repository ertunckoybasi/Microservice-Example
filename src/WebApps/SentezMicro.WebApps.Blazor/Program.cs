using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SentezMicro.WebApps.Blazor.Services.Customer;
using SentezMicro.WebApps.Blazor.Services.Product;
using System.Net.Http;
using System.Threading.Tasks;


namespace SentezMicro.WebApps.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder./Services.AddSingleton<ICustomerService, CustomerService>();
            //builder.Services.AddScoped(sp => new HttpClient { });
            //builder.Services.AddHttpClient();
            ConfigureServices(builder.Services);
            builder.Services.AddTransient<HttpClient>();
            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ICustomerService, CustomerService>();
           
        }
    }
    }

