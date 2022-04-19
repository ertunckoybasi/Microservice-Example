using Services.Order.Domain.AggregateModels.BuyerAggregate;

namespace Services.Order.Application.Interfaces
{
    public interface IBuyerRepository : IGenericRepository<Buyer>
    {
    }
}
