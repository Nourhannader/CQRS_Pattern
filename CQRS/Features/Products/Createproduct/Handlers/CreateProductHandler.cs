using CQRS.Domain.Entities;
using CQRS.Domain.Interfaces;
using CQRS.Features.Products.Createproduct.Commands;
using CQRS.Features.Shared;
using MediatR;

namespace CQRS.Features.Products.Createproduct.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, RequestResponse<bool>>
    {
        private readonly IBaseRepository<Product> _productRespository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(IBaseRepository<Product> productRepository,IUnitOfWork unitOfWork)
        {
            _productRespository = productRepository;
            _unitOfWork=unitOfWork;

        }
        public async Task<RequestResponse<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Dto.Name,
                Price = request.Dto.Price,
                Stock = request.Dto.Stock,
                CategoryId = request.Dto.CategoryId,
            };

            _productRespository.Add(product);

            await _unitOfWork.SaveChangesAsync();
            return RequestResponse<bool>.Success(
                 true,
                "Product created successfully",
                "تم إنشاء المنتج بنجاح"
                );
                
        }
    }
}
