using Grpc.Net.Client;
using GrpcProduct.API.Commands;
using MediatR;
using ProductGrpc.API.Protos;

namespace GrpcProduct.API.Handlers
{
    public class PostProductHandler : IRequestHandler<PostProductCommand, ProductInserted>
    {
        public async Task<ProductInserted> Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7044");

            var client = new Product.ProductClient(channel);

            var proto = new ProductGrpc.API.Protos.ProductModel
            {
                Name = request.Name,
                Price = request.Price
            };

            var product = await client.InsertProductAsync(proto);

            return product;
        }
    }
}
