using EventBus.Base.Events;

namespace SentezMicro.Services.Customer.Application.IntegrationEvents.Events
{
    public class CustomerCreatedIntegrationEvent : IntegrationEvent
    {
        public string CustomerName { get; }
        public string Email { get; }
        public CustomerCreatedIntegrationEvent(string customerName, string email)
        {
            CustomerName = customerName;
            Email = email;
        }
    }
}
