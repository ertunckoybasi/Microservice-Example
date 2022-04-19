using SentezMicro.Services.Customer.Customer.Persistence.Data;
using SentezMicro.Services.Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentezMicro.Services.Customer.Persistence.Data
{
    public class CustomerContexSeed
    {
        public static async Task SeedTask(CustomerContext customerContex)
        {

            if (!customerContex.ErpCustomer.Any())
            {
                customerContex.ErpCustomer.AddRange(GetPreDataCustomersMulti());
            }
            await customerContex.SaveChangesAsync();

        }

        private static IEnumerable<ErpCustomer> GetPreDataCustomers()
        {

            return new List<ErpCustomer>()
            {
              
            
                new ErpCustomer()
                {
                   
                   CurrentAccountName="Ertunç Köybaşı",
                   CurrentAccountEmail="mail@mail.com",
                   CurrentAccountPhone="5427177805",
                   CurrentAccountCode="",
                   CurrentAccountAddress = "Istanbul/Turkey"

                },

                };

        }

        private static IEnumerable<ErpCustomer> GetPreDataCustomersMulti()
        {
            List <ErpCustomer> cusList= new  List<ErpCustomer>();
            for (int i=0;i<50;i++)
            {
                var cusitem = new ErpCustomer()
                {
                    CurrentAccountName = "Ertunç Köybaşı",
                    CurrentAccountCode = i.ToString(),
                    CurrentAccountEmail = "test" + i.ToString() + "@test.com",
                    CurrentAccountPhone = "5427177805",
                    CurrentAccountAddress = "Istanbul/Turkey"

            };

                cusList.Add(cusitem);
            }

            return cusList;
        }

    }

}


