using CQRS.Features.Categories.CreateCategory.Commands;
using CQRS.Features.Categories.CreateCategory.Dtos;
using MediatR;

namespace CQRS.Features.Categories.CreateCategory.Endpoints
{
    public static class CreateCategoryEndpoint
    {
        public static void MapCreateCategoryEndpoint(this WebApplication app)
        {
            app.MapPost("/api/categories",async (CreateCategoryDto dto,IMediator mediator) => {

                var command = new CreateCategoryCommand(dto);
                var result = mediator.Send(command);
                return Results.Ok(result);
            })
            .WithTags("Categories")
            .WithName("CreateCategory")
            .WithSummary("Creates a new category")
            .WithDescription("Creates a new category in the system");
            
        }
    }
}
