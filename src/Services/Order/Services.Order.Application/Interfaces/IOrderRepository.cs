namespace Services.Order.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Domain.AggregateModels.OrderAggregate.Order >
    {
    }
}
