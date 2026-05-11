using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Features.Products.GetProductById.Dtos;
using CQRS.Features.Products.GetProductById.Queries;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.GetProductById.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, RequestResponse<GetProductByIdDto>>
    {
        private readonly IBaseRepository<Product> _productRepository;

        public GetProductByIdQueryHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<RequestResponse<GetProductByIdDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.id);
            if (product == null)
            {
                return RequestResponse<GetProductByIdDto>.Failure(
                     "Product not found",
                    "المنتج غير موجود"
                    );
            }
            var dto = new GetProductByIdDto
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };
            return RequestResponse<GetProductByIdDto>.Success(
                dto,
                "Product retrieved successfully",
                "تم استرجاع المنتج بنجاح"
                );
        }
    }
}
