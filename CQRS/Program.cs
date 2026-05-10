
using CQRS.Domain.Interfaces;
using CQRS.Features.Categories.CreateCategory.Endpoints;
using CQRS.Features.Categories.GetAllCategories.Endpoints;
using CQRS.Infrastructure;
using CQRS.Infrastructure.Repositories;
using CQRS.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddMediatR(typeof(Program).Assembly);

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapCreateCategoryEndpoint();
            app.MapGetAllCategoriesEndpoint();

            app.Run();
        }
    }
}
