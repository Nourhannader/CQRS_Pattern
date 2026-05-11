using CQRS.Features.Products.GetProductById.Queries;
using MediatR;

namespace CQRS.Features.Products.GetProductById.Endpoints
{
    public static class GetProductByIdEndpoint
    {
        public static void MapGetProductByIdEndpoint(this WebApplication app)
        {
            app.MapGet("/api/products/{id}", async (int id, IMediator _mediator) =>
            {
                var result = await _mediator.Send(new GetProductByIdQuery(id));
                return Results.Ok(result);
            })
             .WithName("GetProductById")
            .WithTags("Products");
        }
    }
}
