

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

namespace SentezMicro.WebApps.Blazor.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        
        string apiCreateAddress = "http://localhost:6002/api/customer/Create";
        string apiGetAllAddress = "http://localhost:5000/customer/GetAll";
        //string apiCreateAddress = "http://localhost:5000/customer/Create/";
        public HttpClient _client;

        public CustomerService(HttpClient httpClient)
        {
            _client = httpClient;
            //_client.BaseAddress = new Uri(ApiInfo.CustomerBaseAddress);
        }




        public async Task<Result<List<CustomerVM>>> GetAllCustomersResultModel()
        {

            var response = await _client.GetAsync(apiGetAllAddress);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CustomerVM>>(responseData);
                if (result.Any())
                    return new Result<List<CustomerVM>>(true, ResultConstant.RecordFound, result.ToList());
                return new Result<List<CustomerVM>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<CustomerVM>>(false, ResultConstant.RecordNotFound);

        }

        public async Task<List<CustomerVM>> GetAllCustomers()
        {

            try
            {
                var response = await _client.GetAsync(apiGetAllAddress);
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<CustomerVM>>(responseData);
                return result;
            }
            catch (Exception ex)
            {

                Console.Write(ex.Message);
            }



            return null;
        }

        public async Task<Result<CustomerVM>> CreateCustomer(CustomerVM model)
        {
            try
            {
                var dataAsString = JsonConvert.SerializeObject(model);
                var content = new StringContent(dataAsString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _client.PutAsync(apiCreateAddress, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CustomerVM>(responseData);
                    if (result != null)
                        return new Result<CustomerVM>(true, ResultConstant.RecordCreateSuccessfully, result);
                    else
                        return new Result<CustomerVM>(false, ResultConstant.RecordCreateNotSuccessfully);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Result<CustomerVM>(false, ResultConstant.RecordCreateNotSuccessfully);
        }

        public async Task<List<CustomerVM>> SeedCreateCustomer(int Count)
        {
            List<CustomerVM> CustomerList = new List<CustomerVM>();

            for (int i = 0; i < Count; i++)
            {
                var cusitem = new CustomerVM()
                {
                    CurrentAccountName = "Ertunç Köybaşı",
                    CurrentAccountCode = "Code " + i.ToString(),
                    CurrentAccountEmail = "ertunc.koybasi@sentez.com",
                    CurrentAccountPhone = "5427177805",
                    CurrentAccountAddress = "Istanbul/Turkey"

                };

                CustomerList.Add(cusitem);
                await CreateCustomer(cusitem);
            }
            return CustomerList;
        }


    }

}
