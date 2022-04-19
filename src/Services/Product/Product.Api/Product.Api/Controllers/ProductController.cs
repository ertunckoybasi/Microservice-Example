using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Application.Interfaces;
using Product.Application.Dtos;
using Product.Application.Features.Commands;
using Product.Application.Features.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Product.Api.Controllers
{

    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]

    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IProductRepository ProductRepository, IMediator mediator, IMapper mapper)
        {
            _ProductService = ProductRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var ProductResponse = await _mediator.Send(query);
            if (!ProductResponse.Any()) return NoContent();
            return Ok(ProductResponse);

        }

        [HttpPut]
        [Route("Create")]
        [ProducesResponseType(typeof(IEnumerable<ProductDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NoContent)]
        public async Task<ActionResult<int>> ProductCreate([FromBody] ProductCreateCommand command)
        {
            var ProductResponse = await _mediator.Send(command);
            if (ProductResponse == null) return NoContent();
            return Ok(ProductResponse);

        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<int>> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _ProductService.GetByIdAsync(id);

                if (productToDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                return await _ProductService.DeleteAsync(productToDelete);
            }
            catch (Exception) { 
             return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
