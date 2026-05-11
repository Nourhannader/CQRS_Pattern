using CQRS.Features.Products.GetAllProduct.Queries;
using MediatR;

namespace CQRS.Features.Products.GetAllProduct.Endpoints
{
    public static class GetAllProductsEndpoint
    {
        public static void MapGetAllProductsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/products",async (IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetAllProductQuery());

                return Results.Ok(result);
            })
             .WithName("GetAllProducts")
            .WithTags("Products");
        }
    }
}
