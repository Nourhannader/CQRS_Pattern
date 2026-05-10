using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Features.Categories.GetAllCategories.Dtos;
using CQRS.Features.Categories.GetAllCategories.Queries;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Categories.GetAllCategories.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, RequestResponse<List<GetAllCategoriesDto>>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public GetAllCategoriesHandler(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<RequestResponse<List<GetAllCategoriesDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _categoryRepository.GetAll();
            var categoryDtos = categories.Select(c => new GetAllCategoriesDto { Id = c.Id, Name = c.Name }).ToList();

            return RequestResponse<List<GetAllCategoriesDto>>.Success(categoryDtos);
        }
    }
}
