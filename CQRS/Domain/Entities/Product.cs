using System.ComponentModel.DataAnnotations.Schema;

namespace CQRS.Domain.Entities
{
    public class Product :Base
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        // Foreign key
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        // Navigation property
        public Category Category { get; set; }
    }
}
