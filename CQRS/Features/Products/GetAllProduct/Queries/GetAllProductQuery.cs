using CQRS.Features.Products.GetAllProduct.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.GetAllProduct.Queries
{
    public record GetAllProductQuery : IRequest<RequestResponse<List<GetAllProductDto>>>;
    
}
