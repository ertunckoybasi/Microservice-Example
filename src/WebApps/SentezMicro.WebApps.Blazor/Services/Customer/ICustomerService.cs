

using SentezMicro.Web.Core.Results;
using SentezMicro.Web.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentezMicro.WebApps.Blazor.Services.Customer
{
    public interface ICustomerService
    {
       Task<Result<List<CustomerVM>>> GetAllCustomersResultModel();
      
        Task<List<CustomerVM>> GetAllCustomers();

       Task<Result<CustomerVM>> CreateCustomer(CustomerVM customerVM);

        
        Task<List<CustomerVM>> SeedCreateCustomer(int Count);
    }
}
