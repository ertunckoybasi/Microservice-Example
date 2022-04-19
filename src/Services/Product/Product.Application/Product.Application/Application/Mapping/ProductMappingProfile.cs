using AutoMapper;
using Product.Application.Dtos;
using Product.Application.Features.Commands;
using Product.Domain.Entities;

namespace Product.Application.Application.Mapping
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ErpProduct, ProductDTO>();
            CreateMap<ErpProduct, ProductCreateCommand>();
            CreateMap<ErpProduct, ProductDTO>(); 
            CreateMap<ProductCreateCommand,ErpProduct>();
            CreateMap<ProductDTO, ErpProduct>();

        }
    }
}
