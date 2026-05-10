using CQRS.Features.Categories.CreateCategory.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Categories.CreateCategory.Commands
{
    public record CreateCategoryCommand(CreateCategoryDto Dto) : IRequest<RequestResponse<bool>>;
    
}
