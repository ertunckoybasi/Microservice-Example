using AutoMapper;
using Customer.Api.Application.Responses;
using Customer.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SentezMicro.Services.Customer.Application.Application.Features.Commands;
using SentezMicro.Services.Customer.Application.Application.Features.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Customer.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class customerController : ControllerBase
    {
        private readonly ICustomerRepository _customerService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public customerController(ICustomerRepository customerRepository, IMediator mediator, IMapper mapper)
        {
            _customerService = customerRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers()
        {
            var query = new GetAllCustomersQuery();
            //var customers = await customerRepository.GetAllAsyn();
            var customerResponse = await _mediator.Send(query);
            if (!customerResponse.Any()) return NoContent();
            return Ok(customerResponse);

        }

        [HttpPut]
        [Route("Create")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<CustomerResponse>> CustomerCreate([FromBody] CustomerCreateCommand command)
        {
            var customerResponse = await _mediator.Send(command);
            if (customerResponse==null)  return NoContent();
            return Ok(customerResponse);

        }

   

    }
}