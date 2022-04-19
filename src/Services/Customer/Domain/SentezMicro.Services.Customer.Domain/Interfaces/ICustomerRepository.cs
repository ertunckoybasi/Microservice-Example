using SentezMicro.Services.Customer.Domain.Entities;

namespace Customer.Domain.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<ErpCustomer>
    {
        decimal GetCalcularedSalary();
    }
}