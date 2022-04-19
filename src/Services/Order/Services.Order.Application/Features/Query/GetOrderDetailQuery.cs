using MediatR;
using Services.Order.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Features.Query
{
    public class GetOrderDetailsQuery : IRequest<OrderDetailVM>
    {
        public Guid OrderId { get; set; }

        public GetOrderDetailsQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
