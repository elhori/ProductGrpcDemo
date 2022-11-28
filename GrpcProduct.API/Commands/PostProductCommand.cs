using MediatR;
using ProductGrpc.API.Protos;

namespace GrpcProduct.API.Commands
{
    public class PostProductCommand : IRequest<ProductInserted>
    {
        public string? Name { get; set; }

        public float Price { get; set; }
    }
}
