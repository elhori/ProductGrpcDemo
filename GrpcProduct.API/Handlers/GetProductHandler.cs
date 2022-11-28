using Grpc.Net.Client;
using GrpcProduct.API.Models;
using GrpcProduct.API.Queries;
using MediatR;
using ProductGrpc.API.Protos;

namespace GrpcProduct.API.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, GrpcProduct.API.Models.ProductModel>
    {
        public async Task<GrpcProduct.API.Models.ProductModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7044");

            var client = new Product.ProductClient(channel);

            var product = await client.GetProductInfoAsync(new ProductById { ProductId = request.Id });

            var result = new GrpcProduct.API.Models.ProductModel
            {
                Name = product.Name,
                Price = product.Price
            };

            return result;
        }
    }
}
