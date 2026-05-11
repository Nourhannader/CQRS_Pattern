namespace CQRS.Features.Products.GetAllProduct.Dtos
{
    public class GetAllProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
