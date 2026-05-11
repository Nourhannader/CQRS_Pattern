namespace CQRS.Features.Products.GetProductById.Dtos
{
    public class GetProductByIdDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
