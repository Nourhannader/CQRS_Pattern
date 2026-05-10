using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Infrastructure
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics",CreatedAt=new DateTime(2025,8,1),UpdatedAt=new DateTime(2025,9,25) },
                new Category { Id = 2, Name = "Books",CreatedAt = new DateTime(2025, 8, 1), UpdatedAt = new DateTime(2025, 8, 20) },
                new Category {Id = 3, Name = "Clothes", CreatedAt = new DateTime(2025, 8, 1), UpdatedAt = new DateTime(2025, 9, 25) }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 15000, Stock = 10, CategoryId = 1, CreatedAt = new DateTime(2025, 8, 1), UpdatedAt = new DateTime(2025, 9, 25) },
                new Product { Id = 2, Name = "C# Book", Price = 300, Stock = 50, CategoryId = 2, CreatedAt = new DateTime(2025, 8, 1), UpdatedAt = new DateTime(2025, 8, 20) },
                new Product { Id = 3, Name = "T-Shirt", Price = 200, Stock = 30, CategoryId = 3, CreatedAt = new DateTime(2025, 8, 1), UpdatedAt = new DateTime(2025, 9, 25) }
            );
        }


    }
}
