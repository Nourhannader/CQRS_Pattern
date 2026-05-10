using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
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
        public Task<RequestResponse<bool>> Handle(createproductwithcategoryorchestrator request, CancellationToken cancellationToken)
        {
            var categoryDto=new Create
        }
    }
}
