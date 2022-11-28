using Grpc.Net.Client;
using GrpcProduct.API.Commands;
using GrpcProduct.API.Models;
using GrpcProduct.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductGrpc.API.Protos;

namespace GrpcProduct.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrpcProduct.API.Models.ProductModel>> GetProduct(int id)
        {
            if (id == 0)
                return NotFound($"The product with identifier {id} is not found!");

            var query = new GetProductQuery(id);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("insert-product")]
        public async Task<ActionResult> PostProduct([FromBody] PostProductCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result.Message);
        }
    }
}
