using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Features.Products.GetAllProduct.Dtos;
using CQRS.Features.Products.GetAllProduct.Queries;
using CQRS.Features.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Features.Products.GetAllProduct.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, RequestResponse<List<GetAllProductDto>>>
    {
        private readonly IBaseRepository<Product> _productRepository;

        public GetAllProductHandler(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<RequestResponse<List<GetAllProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAll();
            var productDtos = await products.Select(p => new GetAllProductDto
            {
                Name=p.Name,
                Price=p.Price,
                Stock=p.Stock,
                CategoryId=p.CategoryId
            }).ToListAsync();

            return RequestResponse<List<GetAllProductDto>>.Success(productDtos);
        }
    }
}
