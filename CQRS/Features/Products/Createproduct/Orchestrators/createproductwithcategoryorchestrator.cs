using CQRS.Features.Products.Createproduct.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.Createproduct.Orchestrators
{
    public record createproductwithcategoryorchestrator(createfullproduct dto):IRequest<RequestResponse<bool>>;
    
}
