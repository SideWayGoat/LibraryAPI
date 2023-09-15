using AutoMapper;
using FluentValidation;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryBookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LibraryAPI.BookEndpoints
{
    public static class BookModule
    {
        public static void AddBookEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/book", (LibraryDbContext context) =>
            {
                return context.Books.ToListAsync();
            });
            app.MapGet("/book/{id:int}", async (LibraryDbContext context, int id) =>
            {
                var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if(book != null)
                {
                    return Results.Ok(book);
                }
                return Results.NotFound("The book with this id was not found");
            });
            app.MapPost("/book", async (LibraryDbContext context, CreateBookDTO model,
                IMapper _mapper, [FromServices]IValidator<CreateBookDTO> _validator) =>
            {
                var validationResult = await _validator.ValidateAsync(model);
                if(!validationResult.IsValid)
                {
                    return Results.BadRequest("Invalid input");
                }
                if(context.Books.FirstOrDefault(n => n.Title.ToLower() == model.Title.ToLower()) != null)
                {
                    return Results.BadRequest("Title already exists");
                }
                Book book = _mapper.Map<Book>(model);

                book.Id = context.Books.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

                context.Books.Add(book);
                await context.SaveChangesAsync();

                BookDTO bookDTO = _mapper.Map<BookDTO>(book);

                return Results.Ok(book);
            });
        }
    }
}
