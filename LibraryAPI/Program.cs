
using FluentValidation;
using LibraryAPI.BookEndpoints;
using LibraryAPI.Data;
using LibraryAPI.MapConfig;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<LibraryDbContext>(opt => 
                opt.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.AddBookEndpoints();
            
            app.UseAuthorization();

            app.Run();
        }
    }
}