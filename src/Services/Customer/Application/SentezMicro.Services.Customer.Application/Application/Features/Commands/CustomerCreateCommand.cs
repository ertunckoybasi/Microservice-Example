using Customer.Api.Application.Responses;
using MediatR;

namespace SentezMicro.Services.Customer.Application.Application.Features.Commands
{
    public class CustomerCreateCommand:IRequest<CustomerResponse>
    {
        public int RecId { get; set; }
        public string CurrentAccountCode { get; set; }
        public string CurrentAccountName { get; set; }
        public string CurrentAccountEmail { get; set; }
        public string CurrentAccountPhone { get; set; }
        public string CurrentAccountAddress { get; set; }
    }
}
