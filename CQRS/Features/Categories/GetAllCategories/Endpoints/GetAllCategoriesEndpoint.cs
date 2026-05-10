using CQRS.Features.Categories.GetAllCategories.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Categories.GetAllCategories.Endpoints
{
    public  static class GetAllCategoriesEndpoint
    {
        public static void MapGetAllCategoriesEndpoint(this WebApplication app)
        {
            app.MapGet("/api/Categories", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new Queries.GetAllCategoriesQuery());
                return EndpointResponse<List<GetAllCategoriesDto>>.SuccessResponse(result.Data);
            })
            .WithName("GetAllCategories")
            .WithTags("Categories");
        }
    }
}
