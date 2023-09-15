using LibraryAPI.Data;
using LibraryAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LibraryAPI.BookRepository
{
    public class BookRepo : IBookRepo
    {
        private readonly LibraryDbContext _context;
        public BookRepo(LibraryDbContext context)
        {
            this._context = context;
        }
        public Task<T> CreateBookAsync<T>(CreateBookDTO createBookDTO)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteBookAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable> GetAllBooks<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetBookById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateBookAsync<T>(UpdateBookDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
