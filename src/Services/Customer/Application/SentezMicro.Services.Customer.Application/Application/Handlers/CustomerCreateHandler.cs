using AutoMapper;
using Customer.Api.Application.Responses;
using Customer.Domain.Interfaces;
using EventBus.Base.Abstraction;
using MediatR;
using SentezMicro.Services.Customer.Application.Application.Features.Commands;
using SentezMicro.Services.Customer.Application.IntegrationEvents.Events;
using SentezMicro.Services.Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SentezMicro.Services.Customer.Application.Application.Handlers
{
    public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand,CustomerResponse>
        {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public CustomerCreateHandler(ICustomerRepository customerRepository,IMapper mapper,IEventBus eventBus)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _eventBus = eventBus;

        }

        public async Task<CustomerResponse> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var CustomerEntity = _mapper.Map<ErpCustomer>(request);
            if (CustomerEntity == null)
                throw new ApplicationException("Entity could not be mapped!");

            var Customer = await _customerRepository.AddAsyn(CustomerEntity);

            var CustomerResponse = _mapper.Map<CustomerResponse>(Customer);

            var customerCreatedIntegrationEvent = new CustomerCreatedIntegrationEvent(request.CurrentAccountName,request.CurrentAccountEmail);

            _eventBus.Publish(customerCreatedIntegrationEvent);

            return CustomerResponse;
        }
    }
}
