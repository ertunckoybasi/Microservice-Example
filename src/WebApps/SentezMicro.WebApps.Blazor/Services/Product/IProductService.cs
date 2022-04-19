

using SentezMicro.Web.Core.Results;
using SentezMicro.Web.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentezMicro.WebApps.Blazor.Services.Product
{
    public interface IProductService
    {
       Task<Result<List<ProductVM>>> GetAllProductsResultModel();
       Task<List<ProductVM>> GetAllProducts();

       Task<Result<ProductVM>> CreateProduct(ProductVM ProductVM);

        Task<bool> DeleteProduct(int ID);

        Task<List<ProductVM>> SeedCreateProduct(int Count);
    }
}
