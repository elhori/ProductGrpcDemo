using Grpc.Core;
using ProductGrpc.Server.Protos;

namespace ProductGrpc.Server.Services
{
    public class ProductService : Product.ProductBase
    {
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        public override Task<ProductModel> GetProductInfo(ProductById request, ServerCallContext context)
        {
            ProductModel output = new ProductModel();

            switch (request.ProductId)
            {
                case 1:
                    output.Name = "Lenovo";
                    output.Price = 2000.0f;
                    break;
                case 2:
                    output.Name = "HP";
                    output.Price = 1500.0f;
                    break;
                default:
                    output.Name = "Mac";
                    output.Price = 8000.00f;
                    break;
            }

            return Task.FromResult(output);
        }

        public override Task<ProductInserted> InsertProduct(ProductModel request, ServerCallContext context)
        {
            ProductInserted output = new ProductInserted
            {
                Message = "Product Inserted Succefully"
            };

            return Task.FromResult(output);
        }
    }
}