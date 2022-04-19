using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using SentezMicro.Services.Email.IntegrationEvents.Events;
using SentezMicro.Services.Email.Model;
using SentezMicro.Services.Email.Services;
using System.Threading.Tasks;

namespace SentezMicro.Services.Email.IntegrationEvents.EventHanders
{
    public class CustomerCreatedIntegrationEventHandler : IIntegrationEventHandler<CustomerCreatedIntegrationEvent>
    {
        private readonly ISentezMailService _mailService;
        private readonly ILogger<CustomerCreatedIntegrationEvent> _logger;

        public CustomerCreatedIntegrationEventHandler(ISentezMailService repository, ILogger<CustomerCreatedIntegrationEvent> logger)
        {
            _mailService = repository;
            _logger = logger;
        }

        public async Task Handle(CustomerCreatedIntegrationEvent @event)
        {
            //_logger.LogInformation("----- Handling integration event: {IntegrationEventId} at BCustomerService.Api - ({@IntegrationEvent})", @event.Id, @event);

            MailModel mail = new MailModel
            {
                ToEmail = @event.Email,
                Subject = "Hesabınız oluşturuldu!",
                Body = $"Sayın  {@event.CustomerName} Bu mesaj Rabbitmq'daki  {@event.GetType().Name} dinleyicisi tarafından handle edilmiştir!"
            };
        
            await _mailService.SendEmailAsync(mail);

        }
    }
}
