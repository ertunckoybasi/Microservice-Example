using AutoMapper;
using MediatR;
using Services.Order.Application.Dtos;
using Services.Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Order.Application.Features.Query
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailVM>
    {
        IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper;
        }

        public async Task<OrderDetailVM> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId, i => i.OrderItems);

            var result = mapper.Map<OrderDetailVM>(order);

            return result;
        }
    }
}
