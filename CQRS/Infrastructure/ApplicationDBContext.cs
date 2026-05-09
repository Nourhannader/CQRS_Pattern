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
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" },
                new Category { Id = 3, Name = "Clothes" }
            );

           
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 15000, Stock = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "C# Book", Price = 300, Stock = 50, CategoryId = 2 },
                new Product { Id = 3, Name = "T-Shirt", Price = 200, Stock = 30, CategoryId = 3 }
            );
        }


    }
}
