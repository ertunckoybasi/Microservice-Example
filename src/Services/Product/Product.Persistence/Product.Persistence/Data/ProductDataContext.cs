using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using System.Threading.Tasks;

namespace Product.Persistence.Data
{
    public class ProductDataContext : DbContext
    {
        public DbSet<ErpProduct> erpproducts { get; set; }
        public ProductDataContext() { }
        public ProductDataContext(DbContextOptions<ProductDataContext> options) : base(options)
        {

        }

     
    }
}
