using Microsoft.EntityFrameworkCore;
using SentezMicro.Services.Customer.Domain.Entities;

namespace SentezMicro.Services.Customer.Customer.Persistence.Data
{
    public class CustomerContext : DbContext
    {

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }


        public DbSet<ErpCustomer> ErpCustomer { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //{
        //   //var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    //optionsBuilder.UseSqlServer(@"Data Source =localhost; Initial Catalog = SentezMicro; User Id = sa; Password = 1; Integrated Security = False; "); 
        //    base.OnConfiguring(optionsBuilder); 
        //}

 
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

       
    }
}