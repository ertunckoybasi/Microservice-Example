using AutoMapper;
using MediatR;
using Product.Application.Application.Interfaces;
using Product.Application.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDTO>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(IProductRepository ProductRepository, IMapper mapper)
            {
                this.productRepository = ProductRepository;
                this._mapper = mapper;
            }

            public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var Products = await productRepository.GetAllAsync();
                var cusVM = _mapper.Map<List<ProductDTO>>(Products);
                return cusVM;
            }

        }

        

      

      
    }
}
