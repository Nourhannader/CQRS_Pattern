using CQRS.Features.Products.GetProductById.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.GetProductById.Queries
{
    public record GetProductByIdQuery(int id) : IRequest<RequestResponse<GetProductByIdDto>>;
    
}
