using AutoMapper;
using FluentValidation;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryBookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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
            }).WithName("AllBooks");

            app.MapGet("/book/{Title}", async (LibraryDbContext context, string title) =>
            {
                var book = await context.Books.FirstOrDefaultAsync(x => x.Title.ToLower().Trim() == title.ToLower().Trim());
                if (book != null)
                {
                    return Results.Ok(book);
                }
                return Results.NotFound("The book with this title was found, perhaps you spelled it wrong?");
            }).WithName("Find Title");

            app.MapPost("/book", async (LibraryDbContext context, CreateBookDTO model,
                IMapper _mapper, [FromServices] IValidator<CreateBookDTO> _validator) =>
            {
                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest("Invalid input");
                }
                if (context.Books.FirstOrDefault(n => n.Title.ToLower() == model.Title.ToLower()) != null)
                {
                    return Results.BadRequest("Title already exists");
                }
                Book book = _mapper.Map<Book>(model);

                context.Books.Add(book);
                await context.SaveChangesAsync();

                BookDTO bookDTO = _mapper.Map<BookDTO>(book);

                return Results.Ok(book);
            });

            app.MapDelete("/book", async (LibraryDbContext context, int id) =>
            {
                var result = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (result != null)
                {
                    context.Books.Remove(result);
                    await context.SaveChangesAsync();
                    return Results.Ok(result);
                }
                return Results.BadRequest("No book had that ID");
            }).WithName("Delete");
        }
    }
}
