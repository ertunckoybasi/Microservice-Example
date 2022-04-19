using AutoMapper;
using EventBus.Base.Abstraction;
using MediatR;
using Product.Application.Application.IntegrationEvents;
using Product.Application.Application.Interfaces;
using Product.Application.Dtos;
using Product.Application.Features.Commands;
using Product.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Handlers
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand,ProductDTO>
        {
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public ProductCreateHandler(IProductRepository ProductRepository,IMapper mapper,IEventBus eventBus)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
            _eventBus = eventBus;

        }

        public async Task<ProductDTO> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var ProductEntity = _mapper.Map<ErpProduct>(request);
            if (ProductEntity == null)
                throw new ApplicationException("Entity could not be mapped!");

            var Product = await _ProductRepository.CreateAsync(ProductEntity);

            var ProductResponse = _mapper.Map<ProductDTO>(Product);

            var productCreatedIntegrationEvent = new ProductCreatedIntegrationEvent(request.RecId, request.ProductName);

            _eventBus.Publish(productCreatedIntegrationEvent);

            return ProductResponse;
        }

      
    }
}
