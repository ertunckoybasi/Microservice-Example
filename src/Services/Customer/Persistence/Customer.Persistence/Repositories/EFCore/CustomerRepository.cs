using Customer.Domain.Interfaces;
using SentezMicro.Services.Customer.Customer.Persistence.Data;
using SentezMicro.Services.Customer.Domain.Entities;

namespace Customer.Persistence.Repositories.EFCore
{
    public class CustomerRepository : GenericRepository<ErpCustomer>, ICustomerRepository
    {

        public CustomerRepository(CustomerContext context) : base(context)
        {

        }
        public decimal GetCalcularedSalary()
        {
            decimal mdSalary = 5000;
            return  mdSalary;
        }
    }
}