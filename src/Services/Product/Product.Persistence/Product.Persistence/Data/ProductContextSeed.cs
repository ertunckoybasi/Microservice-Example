using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Persistence.Data
{
    public class ProductContextSeed
    {

        public static async Task SeedTask(ProductDataContext productContex)
        {

            if (!productContex.erpproducts.Any())
            {
                productContex.erpproducts.AddRange(GetPreDataProductsMulti());
            }
            await productContex.SaveChangesAsync();

        }
        private static IEnumerable<ErpProduct> GetPreDataProductsMulti()
        {
            List<ErpProduct> cusList = new List<ErpProduct>();
            for (int i = 0; i < 10; i++)
            {
                var cusitem = new ErpProduct()
                {
                    //RecId = i,
                    ProductName = "Sentez Product " + i.ToString(),
                   Price = i*100 ,
                   Barcode = "BRC" + i.ToString() ,
                   Description = "Açıklama " + i.ToString()
                };

                cusList.Add(cusitem);
            }

            return cusList;
        }
    }
}
