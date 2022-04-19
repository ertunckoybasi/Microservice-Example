using MediatR;
using System;
using Services.Order.Domain.AggregateModels.OrderAggregate;

namespace Services.Order.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {
        public string UserName { get; }
        public int CardTypeId { get; }
        public string CardNumber { get; }
        public string CardSecurityNumber { get; }
        public string CardHolderName { get; }
        public DateTime CardExpiration { get; }
        public AggregateModels.OrderAggregate.Order Order { get; }

        public OrderStartedDomainEvent(AggregateModels.OrderAggregate.Order order, string userName,
                                       int cardTypeId, string cardNumber,
                                       string cardSecurityNumber, string cardHolderName,
                                       DateTime cardExpiration)
        {
            Order = order;
            UserName = userName;
            CardTypeId = cardTypeId;
            CardNumber = cardNumber;
            CardSecurityNumber = cardSecurityNumber;
            CardHolderName = cardHolderName;
            CardExpiration = cardExpiration;
        }
    }
}
