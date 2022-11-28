using Grpc.Net.Client;
using GrpcProduct.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductGrpc.API.Protos;
using ProductModel = GrpcProduct.API.Models.ProductModel;

namespace GrpcProduct.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            if (id == 0)
                return NotFound($"The product with identifier {id} is not found!");

            var channel = GrpcChannel.ForAddress("https://localhost:7044");

            var client = new Product.ProductClient(channel);

            var product = await client.GetProductInfoAsync(new ProductById { ProductId = id });

            var result = new ProductModel
            {
                Name = product.Name,
                Price = product.Price
            };

            return Ok(new { product = result });
        }

        [HttpPost("insert-product")]
        public async Task<ActionResult> PostProduct([FromBody] ProductModel model)
        {
            if(!ModelState.IsValid)
            {
                return Problem("Model state is unvalid!");
            }

            var channel = GrpcChannel.ForAddress("https://localhost:7044");

            var client = new Product.ProductClient(channel);

            var proto = new ProductGrpc.API.Protos.ProductModel
            {
                Name = model.Name,
                Price = model.Price
            };

            var product = await client.InsertProductAsync(proto);

            return Ok(product.Message);
        }
    }
}
