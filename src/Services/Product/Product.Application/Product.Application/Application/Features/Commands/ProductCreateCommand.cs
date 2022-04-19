
using MediatR;
using Product.Application.Dtos;

namespace Product.Application.Features.Commands
{
    public class ProductCreateCommand:IRequest<ProductDTO>
    {
        public int RecId { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

    }
}
