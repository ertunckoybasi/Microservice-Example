using AutoMapper;
using Customer.Api.Application.Responses;
using SentezMicro.Services.Customer.Application.Application.Features.Commands;
using SentezMicro.Services.Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentezMicro.Services.Customer.Application.Application.Mapping
{
   public class CustomerMappingProfile:Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<ErpCustomer, CustomerResponse>();
            CreateMap<ErpCustomer, CustomerCreateCommand>();
            CreateMap<ErpCustomer, CustomerResponse>(); 
            CreateMap<CustomerCreateCommand,ErpCustomer>();
            CreateMap<CustomerResponse, ErpCustomer>();

        }
    }
}
