using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Features.Categories.CreateCategory.Commands;
using CQRS.Features.Categories.CreateCategory.Dtos;
using CQRS.Features.Products.Createproduct.Commands;
using CQRS.Features.Products.Createproduct.Dtos;
using CQRS.Features.Products.Createproduct.Orchestrators;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.Createproduct.Handlers
{
    public class createproductwithcategoryorchestratorHandler : IRequestHandler<createproductwithcategoryorchestrator, RequestResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Category> _categoryRepository;
        public createproductwithcategoryorchestratorHandler(IMediator mediator,IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mediator = mediator;
        }
        public async Task<RequestResponse<bool>> Handle(createproductwithcategoryorchestrator request, CancellationToken cancellationToken)
        {
            var categoryDto = new CreateCategoryDto { Name = request.dto.Name };
            var categorycommand = await _mediator.Send(new CreateCategoryCommand(categoryDto));

            var Categoryobj = _categoryRepository.GetAll().FirstOrDefault(c => c.Name == request.dto.Name);
            int CategoryId = Categoryobj.Id;

            var GetAllProductDto = new CreateProductDto
            {
                Name = request.dto.Name,
                Price = request.dto.Price,
                Stock = request.dto.Stock,
                CategoryId = CategoryId,
            };
            var productCommand = _mediator.Send(new CreateProductCommand(GetAllProductDto));
            return RequestResponse<bool>.Success(
                true,
                "Product with category created successfully",
                "تم إنشاء المنتج مع الفئة بنجاح"
            );
        }
    }
}
