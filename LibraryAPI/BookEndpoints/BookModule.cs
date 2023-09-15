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
            app.MapGet("/book/", (LibraryDbContext context) =>
            {
                return context.Books.ToListAsync();
            }).WithName("AllBooks");

            app.MapGet("/book/search/{Title}", async (LibraryDbContext context, string title, IMapper _mapper) =>
            {
                var book = await context.Books.FirstOrDefaultAsync(x => x.Title.ToLower().Trim() == title.ToLower().Trim());

                var bookDTO = _mapper.Map<BookDTO>(book);

                if (book != null)
                {
                    return Results.Ok(bookDTO);
                }
                return Results.NotFound("The book with this title was found, perhaps you spelled it wrong?");
            }).WithName("Find Title");

            app.MapGet("/book/search/{Author}/books", async (LibraryDbContext context, string author, IMapper _mapper) =>
            {
                var book = await context.Books.Where(x => x.Author == author).ToListAsync();

                var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(book);
                
                if (book != null)
                {
                    return Results.Ok(bookDTO);
                }
                return Results.NotFound("The book from this author was found, perhaps you spelled it wrong?");
            }).WithName("Find author");

            app.MapPost("/book/", async (LibraryDbContext context, CreateBookDTO model,
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
            }).WithName("Add new book to database");

            app.MapDelete("/book/", async (LibraryDbContext context, int id) =>
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

            app.MapPut("/book/", async (LibraryDbContext context, int id, UpdateBookDTO model,
                IMapper _mapper, [FromServices] IValidator<UpdateBookDTO> _validator) =>
            {
                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest("Invalid input or something ");
                }
                var bookToUpdate = context.Books.FirstOrDefault(x => x.Id == id);
                bookToUpdate = _mapper.Map<Book>(model);

                context.Update(bookToUpdate);

                await context.SaveChangesAsync();

                return Results.Ok(bookToUpdate);

            }).WithName("Update Book Information");
        }
    }
}
