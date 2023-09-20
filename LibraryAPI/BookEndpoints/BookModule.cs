using AutoMapper;
using FluentValidation;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryBookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.BookEndpoints
{
    public static class BookModule
    {
        public static void AddBookEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/book/all", async (LibraryDbContext context, IMapper _mapper) =>
            {
                ApiResponse response = new ApiResponse();
                var books = await context.Books.ToListAsync();
                var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(books);
                response.Result = bookDTO;
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;


                return Results.Ok(response);
            }).WithName("AllBooks");

            app.MapGet("/book/search/", async (LibraryDbContext context, string search, IMapper _mapper) =>
            {
                ApiResponse response = new ApiResponse();
                var book = await context.Books.Where(s => (s.Title == search || s.Author == search || s.Genre == search)).ToListAsync();

                if (book.Any())
                {
                    var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(book);
                    response.Result = bookDTO;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                response.ErrorMessages.Add("No title was found with that name");
                return Results.NotFound(response);
            }).WithName("Search");

            //app.MapGet("/book/search/{Author}/books", async (LibraryDbContext context, string author, IMapper _mapper) =>
            //{
            //    ApiResponse response = new ApiResponse();
            //    var book = await context.Books.Where(x => x.Author == author).ToListAsync();

            //    if (book.Any())
            //    {
            //        var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(book);
            //        response.Result = bookDTO;
            //        response.IsSuccess = true;
            //        response.StatusCode = System.Net.HttpStatusCode.OK;
            //        return Results.Ok(bookDTO);
            //    }
            //    response.ErrorMessages.Add("No book from this author was found, perhaps you spelled it wrong?");
            //    return Results.NotFound(response);
            //}).WithName("search");

            app.MapPost("/book/create", async (LibraryDbContext context, CreateBookDTO model,
                IMapper _mapper, [FromServices] IValidator<CreateBookDTO> _validator) =>
            {
                ApiResponse response = new ApiResponse();
                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(response);
                }
                if (context.Books.FirstOrDefault(n => n.Title.ToLower() == model.Title.ToLower()) != null)
                {
                    response.ErrorMessages.Add("Title already exists");
                    return Results.BadRequest(response);

                }
                Book book = _mapper.Map<Book>(model);

                context.Books.Add(book);
                await context.SaveChangesAsync();

                BookDTO bookDTO = _mapper.Map<BookDTO>(book);
                response.Result = bookDTO;
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }).WithName("create");

            app.MapDelete("/book/delete", async (LibraryDbContext context, int id) =>
            {
                ApiResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

                var bookToDelete = await context.Books.FirstOrDefaultAsync(n => n.Id == id);
                if (bookToDelete != null)
                {
                    context.Remove(bookToDelete);
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                response.ErrorMessages.Add("invalid ID");
                return Results.BadRequest(response);

            }).WithName("Delete");

            app.MapPut("/book/update", async (LibraryDbContext context, int id, UpdateBookDTO model,
                IMapper _mapper, [FromServices] IValidator<UpdateBookDTO> _validator) =>
            {
                ApiResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                }

                var bookToUpdate = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
                bookToUpdate.NumberInStock = model.NumberInStock;
                bookToUpdate.Author = model.Author;
                bookToUpdate.Description = model.Description;
                bookToUpdate.Title = model.Title;
                bookToUpdate.Genre = model.Genre;

                await context.SaveChangesAsync();

                var book = _mapper.Map<Book>(model);

                response.Result = _mapper.Map<BookDTO>(bookToUpdate);
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);


            }).WithName("Update Book Information");
        }
    }
}
