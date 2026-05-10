using CQRS.Features.Categories.GetAllCategories.Dtos;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Categories.GetAllCategories.Queries
{
    public record GetAllCategoriesQuery :IRequest<RequestResponse<List<GetAllCategoriesDto>>>;
    
}
