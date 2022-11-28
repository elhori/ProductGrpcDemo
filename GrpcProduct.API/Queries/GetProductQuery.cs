using GrpcProduct.API.Models;
using MediatR;

namespace GrpcProduct.API.Queries
{
    public class GetProductQuery : IRequest<ProductModel>
    {
        public int Id { get; set; }

        public GetProductQuery(int Id)
        {
            this.Id = Id;
        }
    }
}
