using CQRS.Features.Products.Createproduct.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.Createproduct.Commands
{
    public record CreateProductCommand(CreateProductDto Dto) : IRequest<RequestResponse<bool>>;
    
}
