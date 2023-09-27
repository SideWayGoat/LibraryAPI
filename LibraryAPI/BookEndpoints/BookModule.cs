using AutoMapper;
using FluentValidation;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Services;
using LibraryBookModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.BookEndpoints
{
    public static class BookModule
    {
        public static void AddBookEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/book/all", async (IBookRepository _repo, IMapper _mapper) =>
            {
                ApiResponse response = new ApiResponse();
                var books = await _repo.GetAllBooks();
                var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(books);
                response.Result = bookDTO;
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;


                return Results.Ok(response);
            }).WithName("AllBooks").Produces(200);

            app.MapGet("/book/search/{search}", async (IBookRepository _repo, string search, IMapper _mapper) =>
            {
                ApiResponse response = new ApiResponse();
                var book = await _repo.GetBySearch(search);

                if (book.Any())
                {
                    var bookDTO = _mapper.Map<IEnumerable<BookDTO>>(book);
                    response.Result = bookDTO;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                response.ErrorMessages.Add("No match was found, did you spell correctly?");
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

            app.MapPost("/book/create", async (IBookRepository _repo, [FromBody]CreateBookDTO model,
                IMapper _mapper, [FromServices] IValidator<CreateBookDTO> _validator) =>
            {
                ApiResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
                var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(response);
                }
                Book book = _mapper.Map<Book>(model);

                var result = await _repo.CreateBook(book);

                BookDTO bookDTO = _mapper.Map<BookDTO>(result);
                response.Result = bookDTO;
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);
            }).WithName("create");

            app.MapDelete("/book/delete/{id}", async (IBookRepository _repo, int id) =>
            {
                ApiResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest};
                try
                {
					var bookToDelete = await _repo.DeleteBook(id);
					if (bookToDelete != null)
					{
						response.IsSuccess = true;
						response.StatusCode = System.Net.HttpStatusCode.OK;
                        response.Result = bookToDelete;
						return Results.Ok(response);
					}
					response.ErrorMessages.Add("invalid ID");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
					return Results.NotFound(response);
				}
                catch (Exception e)
                {

                    return Results.BadRequest(e);
                }


            }).WithName("Delete");

            app.MapPut("/book/update/{id}", async (IBookRepository _repo, int id, UpdateBookDTO model,
                IMapper _mapper, [FromServices] IValidator<UpdateBookDTO> _validator) =>
            {
                ApiResponse response = new() { IsSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
				var validationResult = await _validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                }
                var bookToUpdate = _mapper.Map<Book>(model);
                var result = await _repo.UpdateBook(bookToUpdate);

                response.Result = _mapper.Map<BookDTO>(bookToUpdate);
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Results.Ok(response);


            }).WithName("Update Book Information");
        }
    }
}
