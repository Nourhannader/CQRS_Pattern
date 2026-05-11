using CQRS.Features.Products.Createproduct.Dtos;
using CQRS.Features.Products.Createproduct.Orchestrators;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.Createproduct.Endpoints
{
    public static class CreateProductEndpoint
    {
        public static void MapCreateProductEndpoint( this WebApplication app)
        {
            app.MapPost("/api/Product", async (IMediator mediator, createfullproduct dto) =>
            {
                var result = await mediator.Send(new createproductwithcategoryorchestrator(dto));
                return EndpointResponse<bool>.SuccessResponse(
                    result.IsSuccess
                );
            })
           .WithName("CreateProduct")
           .WithTags("Products");
        }
    }
}
