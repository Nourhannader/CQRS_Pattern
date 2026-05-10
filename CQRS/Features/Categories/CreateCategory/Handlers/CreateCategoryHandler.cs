using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Features.Categories.CreateCategory.Commands;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Categories.CreateCategory.Handlers
{

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, RequestResponse<bool>>
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(IBaseRepository<Category> categoryRepository,IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Dto.Name,
            };

            _categoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();

            return RequestResponse<bool>.Success(
                true,
                "Category created successfully",
                "تم إنشاء التصنيف بنجاح"
            );
        }
    }
}
