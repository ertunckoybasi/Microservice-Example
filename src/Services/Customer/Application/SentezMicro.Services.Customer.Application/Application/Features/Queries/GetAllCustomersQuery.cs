using AutoMapper;
using Customer.Api.Application.Responses;
using Customer.Domain.Interfaces;
using MediatR;
using SentezMicro.Services.Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SentezMicro.Services.Customer.Application.Application.Features.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerResponse>>
    {
        public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponse>>
        {
            private readonly ICustomerRepository customerRepository;
            private readonly IMapper _mapper;
            public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
            {
                this.customerRepository = customerRepository;
                this._mapper = mapper;
            }

            public async Task<List<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await customerRepository.GetAllAsyn();
                var cusVM = _mapper.Map<List<CustomerResponse>>(customers);
                return cusVM;
            }

        }

        

      

      
    }
}
