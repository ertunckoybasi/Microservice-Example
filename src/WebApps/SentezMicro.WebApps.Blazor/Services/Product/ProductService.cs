

using Newtonsoft.Json;
using SentezMicro.Web.Core.Common;
using SentezMicro.Web.Core.Results;
using SentezMicro.Web.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SentezMicro.WebApps.Blazor.Services.Product
{
    public class ProductService : IProductService
    {

        string apiDeleteAddress = "http://localhost:6007/api/product/delete/";
        string apiCreateAddress = "http://localhost:6007/api/product/Create";
        string apiGetAllAddress = "http://localhost:5000/product/GetAll";
        //string apiCreateAddress = "http://localhost:5000/product/Create/";
        public HttpClient _client;

        public ProductService(HttpClient httpClient)
        {
            _client = httpClient;
            //_client.BaseAddress = new Uri(ApiInfo.ProductBaseAddress);
        }




        public async Task<Result<List<ProductVM>>> GetAllProductsResultModel()
        {

            var response = await _client.GetAsync(apiGetAllAddress);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ProductVM>>(responseData);
                if (result.Any())
                    return new Result<List<ProductVM>>(true, ResultConstant.RecordFound, result.ToList());
                return new Result<List<ProductVM>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<ProductVM>>(false, ResultConstant.RecordNotFound);

        }

        public async Task<List<ProductVM>> GetAllProducts()
        {

            try
            {
                var response = await _client.GetAsync(apiGetAllAddress);
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ProductVM>>(responseData);
                return result;
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
            }



            return null;
        }

        public async Task<Result<ProductVM>> CreateProduct(ProductVM model)
        {
           // try
            {
                var dataAsString = JsonConvert.SerializeObject(model);
                var content = new StringContent(dataAsString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _client.PutAsync(apiCreateAddress, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ProductVM>(responseData);
                    if (result != null)
                        return new Result<ProductVM>(true, ResultConstant.RecordCreateSuccessfully, result);
                    else
                        return new Result<ProductVM>(false, ResultConstant.RecordCreateNotSuccessfully);
                }

            }
            //catch (Exception ex)
           // {
              // Console.WriteLine(ex.Message);
            //}
            return new Result<ProductVM>(false, ResultConstant.RecordCreateNotSuccessfully);
        }

        public async Task<List<ProductVM>> SeedCreateProduct(int Count)
        {
            List<ProductVM> ProductList = new List<ProductVM>();

            for (int i = 0; i < Count; i++)
            {
                var cusitem = new ProductVM()
                {
                    ProductName = "Sentez Product " + i.ToString(),
                    Description= "Description " + i.ToString(),
                    Price = i*10,
                    Barcode = "XXXXX-"+i.ToString()
                };

                ProductList.Add(cusitem);
                await CreateProduct(cusitem);
            }
            return ProductList;
        }

        public async Task<bool> DeleteProduct(int ID)
        {
            using var response = await _client.DeleteAsync(apiDeleteAddress + ID.ToString());

            if (!response.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return
                string errorMessage = response.ReasonPhrase;
                return false;
            }

            return true;
        }
    }

}
